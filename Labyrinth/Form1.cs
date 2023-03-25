using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Automation;
using System.Xml.Serialization;

namespace Labyrinth
{
    public enum GameState { None, Hover, Waiting, FirstPart, SecondPart };
    public enum GameMode { Easy, Normal, Hard };
    public enum GameBonusType { Clock, Light, Diamond };

    public partial class Form1 : Form
    {
        private const string orderFileName = "order.xml";

        public sealed class Order
        {
            public int DiamondsCount { get; set; }
            public TimeSpan BestTime { get; set; }
            public int WinsCount { get; set; }

            public Order() { }

            public Order(int diamondsCount, TimeSpan bestTime, int winsCount)
            {
                DiamondsCount = diamondsCount;
                BestTime = bestTime;
                WinsCount = winsCount;
            }
        }

        private readonly List<Collision> collisions;
        private readonly List<Collision> finishCollisions;
        private Collision? startCollision, endCollision;
        private Collision? bonusCollision;
        private GameState gameState;
        private GameMode gameMode = GameMode.Easy;
        private GameBonusType bonusType;

        private SolidBrush targetBrush = new SolidBrush(Color.DarkRed);
        private SolidBrush collisionBrush = new SolidBrush(Color.DarkBlue);

        private int viewRadius;
        private Point cursorPos;
        private TimeSpan time;
        private TimeSpan elapsedTime;

        private readonly Order order;

        public Form1()
        {
            InitializeComponent();

            if (File.Exists(orderFileName))
            {
                try
                {
                    using (Stream s = File.OpenRead(orderFileName))
                    {
                        var serializer = new XmlSerializer(typeof(Order));
                        if (serializer.Deserialize(s) is Order order)
                        {
                            this.order = order;
                        }
                        else
                        {
                            s.Close();
                            File.Delete(orderFileName);
                        }
                    }
                }
                catch
                {
                    File.Delete(orderFileName);
                }
            }

            order ??= new Order();
            UpdateResultLabels();

            collisions = new List<Collision>()
            {
                new Collision(0, 0, 624, 12),
                new Collision(0, 0, 12, 624),
                new Collision(612, 0, 12, 624),
                new Collision(0, 612, 624, 12),

                new Collision(62, 62, 100, 12),
                new Collision(412, 212, 100, 12),
                new Collision(512, 112, 100, 12),
                new Collision(212, 12, 12, 100),
                new Collision(162, 62, 12, 100),
                new Collision(162, 212, 12, 100),
                new Collision(212, 312, 12, 100),
                new Collision(312, 362, 12, 100),
                new Collision(512, 462, 12, 100),
                new Collision(462, 62, 12, 100),
                new Collision(362, 162, 12, 100),
                new Collision(562, 262, 12, 150),
                new Collision(462, 312, 12, 300),
                new Collision(412, 262, 12, 300),
                new Collision(62, 312, 12, 150),
                new Collision(212, 162, 12, 100),
                new Collision(212, 462, 12, 100),
                new Collision(262, 512, 12, 100),
                new Collision(262, 62, 150, 12),
                new Collision(512, 62, 50, 12),
                new Collision(562, 162, 50, 12),
                new Collision(512, 62, 50, 12),
                new Collision(362, 112, 50, 12),
                new Collision(512, 62, 50, 12),
                new Collision(562, 462, 50, 12),
                new Collision(512, 562, 50, 12),
                new Collision(162, 112, 50, 12),
                new Collision(412, 12, 12, 50),
                new Collision(362, 62, 12, 50),
                new Collision(312, 112, 12, 50),
                new Collision(262, 112, 12, 50),
                new Collision(62, 162, 12, 50),
                new Collision(112, 212, 12, 50),
                new Collision(412, 162, 12, 50),
                new Collision(562, 162, 12, 50),
                new Collision(512, 112, 12, 50),
                new Collision(262, 412, 12, 50),
                new Collision(362, 312, 12, 50),
                new Collision(112, 362, 12, 50),
                new Collision(162, 362, 12, 50),
                new Collision(62, 512, 12, 50),
                new Collision(162, 462, 12, 50),
                new Collision(512, 412, 100, 12),
                new Collision(512, 512, 100, 12),
                new Collision(12, 112, 100, 12),
                new Collision(62, 162, 150, 12),
                new Collision(262, 512, 100, 12),
                new Collision(12, 262, 100, 12),
                new Collision(212, 212, 100, 12),
                new Collision(212, 262, 100, 12),
                new Collision(262, 162, 200, 12),
                new Collision(412, 262, 150, 12),
                new Collision(62, 312, 100, 12),
                new Collision(62, 462, 100, 12),
                new Collision(12, 512, 150, 12),
                new Collision(112, 412, 150, 12),
                new Collision(212, 312, 150, 12),
                new Collision(262, 362, 150, 12),
                new Collision(112, 562, 150, 12),
                new Collision(312, 562, 100, 12),
                new Collision(362, 412, 12, 100),
                new Collision(462, 312, 50, 12),
                new Collision(462, 362, 50, 12),
                new Collision(562, 12, 12, 50),
            };

            finishCollisions = new List<Collision>()
            {
                new Collision(12, 12, 50, 50, true, false),
                new Collision(562, 112, 50, 50, true, false),
                new Collision(562, 462, 50, 50, true, false),
                new Collision(312, 312, 50, 50, true, false),
            };

            timer1.Interval = 1000;

            foreach(var ob in Enum.GetNames(typeof(GameMode)))
            {
                mode_comboBox.Items.Add(ob);
            }
            mode_comboBox.SelectedItem = GameMode.Normal.ToString();
        }

        private void UpdateResultLabels()
        {
            diamonds_label.Text = $"Diamonds: {order.DiamondsCount.ToString("00")}";
            bestTime_label.Text = $"Best time: {order.BestTime.TotalSeconds.ToString("00")}";
            winsCount_label.Text = $"Number of wins: {order.WinsCount.ToString("00")}";
        }

        private void SaveOrder()
        {
            try
            {
                using (Stream s = File.Create(orderFileName))
                {
                    var serilizer = new XmlSerializer(typeof(Order));
                    serilizer.Serialize(s, order);
                }
            }
            catch
            {
                if (File.Exists(orderFileName))
                    File.Delete(orderFileName);
            }
        }

        private void ResetGame()
        {
            button1.Enabled = mode_comboBox.Enabled = true;
            elapsedTime = TimeSpan.Zero;
            button1.Text = "Start";
            gameState = GameState.None;
            finishCollisions.ForEach(i => i.IsEnabled = false);
            UpdateStatusLabel();
            pictureBox1.Invalidate();
        }

        private void WinGame(string message)
        {
            if (order.BestTime.TotalSeconds == 0 || elapsedTime.TotalSeconds < order.BestTime.TotalSeconds)
                order.BestTime = elapsedTime;
            order.WinsCount++;
            UpdateResultLabels();
            SaveOrder();

            ResetGame();

            message += "\nRestart game?";
            if (MessageBox.Show(message, "You win!", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                StartHover();
            UpdateStatusLabel();
        }

        private void GameOver(string message)
        {
            ResetGame();
            message += "\nRestart game?";
            if (MessageBox.Show(message, "You lose", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                StartHover();
            UpdateStatusLabel();
        }

        private void DrawEndPoint(Graphics g, Rectangle rect)
        {
            g.FillEllipse(Brushes.DarkGreen, rect);
        }

        private void DrawDiamond(Graphics g, Rectangle rect)
        {
            var path = new GraphicsPath();
            path.AddPolygon(new Point[]
            {
                new Point((int)(rect.Left + rect.Width * 0.5d), rect.Top),
                new Point(rect.Left, (int)(rect.Top + rect.Width * 0.5d)),
                new Point((int)(rect.Left + rect.Width * 0.5d), rect.Bottom),
                new Point(rect.Right, (int)(rect.Top + rect.Width * 0.5d)),
            });
            using (Brush brush = new SolidBrush(Color.FromArgb(3, 132, 252)))
            {
                g.FillPath(brush, path);

                var reg = new Region(new Rectangle((int)(rect.Left + rect.Width * 0.5d), rect.Top, (int)(rect.Width * 0.5d), (int)(rect.Height * 0.5d)));
                reg.Intersect(path);
                g.FillRegion(new SolidBrush(Color.FromArgb(100, Color.White)), reg);

                reg = new Region(new Rectangle(rect.Left, (int)(rect.Top + rect.Height * 0.5d), (int)(rect.Width * 0.5d), (int)(rect.Height * 0.5d)));
                reg.Intersect(path);
                g.FillRegion(new SolidBrush(Color.FromArgb(100, Color.Black)), reg);
            }
        }

        private void DrawClock(Graphics g, Rectangle rect)
        {
            g.FillEllipse(Brushes.White, rect);
            using (Pen pen = new Pen(Color.DarkRed, 3))
            {
                var centerRect = new Rectangle((int)(rect.Left + rect.Width * 0.1d), (int)(rect.Left + rect.Height * 0.1d), (int)(rect.Width * (1 - 0.2)), (int)(rect.Height * (1 - 0.2)));

                var path1 = new GraphicsPath();
                path1.AddEllipse(centerRect);

                var path2 = new GraphicsPath();
                path2.AddEllipse(rect);

                var reg = new Region(path1);
                reg.Xor(path2);

                g.FillRegion(new LinearGradientBrush(new Point(rect.Right, rect.Top), new Point(rect.Left, rect.Bottom), Color.FromArgb(189, 0, 0), Color.FromArgb(139, 0, 0)), reg);
                var center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                g.DrawLine(pen, center, new Point(center.X + rect.Width / 2, center.Y));
                g.DrawLine(pen, center, new Point(center.X, center.Y - rect.Height / 2));
            }
        }

        private void DrawMedal(Graphics g, Rectangle rect)
        {
            using (var brush = new LinearGradientBrush(new Point(rect.Left, rect.Bottom), new Point(rect.Right, rect.Top), Color.FromArgb(240, 162, 17), Color.FromArgb(255, 179, 38)))
            {
                g.FillEllipse(brush, rect);
                var font = new Font("Georgia", 18);

                var size = g.MeasureString("1", font);
                var linearBrush = new LinearGradientBrush(new Point(rect.Right, rect.Top), new Point(rect.Right, rect.Bottom), Color.FromArgb(214, 124, 6), Color.FromArgb(166, 95, 3));
                g.DrawString("1", font, linearBrush, new Point(rect.Left + rect.Width / 2 - (int)(size.Width / 2), rect.Top + rect.Height / 2 - (int)(size.Height / 2)));
            }
        }

        private void DrawBonus(Graphics g, Rectangle rect)
        {
            switch (bonusType)
            {
                case GameBonusType.Clock:
                    g.FillEllipse(Brushes.White, rect);
                    using (Pen pen = new Pen(Color.Blue, 4))
                    {
                        g.DrawEllipse(pen, rect);
                        var center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                        g.DrawLine(pen, center, new Point(rect.Right - 15, center.Y));
                        g.DrawLine(pen, center, new Point(center.X, rect.Top + 10));
                    }
                    break;
                case GameBonusType.Light:

                    using (Pen pen = new Pen(Color.FromArgb(235, 164, 52), 4))
                    {
                        var center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                        using (var t = new Matrix())
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                t.RotateAt(360 * (i / 8f), center);
                                g.Transform = t;
                                g.DrawLine(pen, center, new Point(center.X, rect.Top));
                            }
                        }
                        g.ResetTransform();
                        var centerRect = new Rectangle((int)(rect.X + rect.Width * 0.25d), (int)(rect.Y + rect.Height * 0.25d), (int)(rect.Width * 0.5d), (int)(rect.Height * 0.5d));
                        g.FillEllipse(Brushes.White, centerRect);
                        g.DrawEllipse(pen, centerRect);
                    }
                    break;
                case GameBonusType.Diamond:
                    DrawDiamond(g, rect);
                    break;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            finishCollisions.ForEach(i => i.Fill(g, targetBrush));
            if (gameState == GameState.FirstPart)
            {
                if(endCollision != null)
                    DrawEndPoint(g, endCollision.Bounds);
                if (bonusCollision != null)
                    DrawBonus(g, bonusCollision.Bounds);
            }
                
            collisions.ForEach(i => i.Fill(g, collisionBrush));

            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (gameState == GameState.FirstPart || gameState == GameState.SecondPart)
            {
                var path = new GraphicsPath();
                path.AddEllipse(new Rectangle(new Point(cursorPos.X - viewRadius / 2, cursorPos.Y - viewRadius / 2), new Size(viewRadius, viewRadius)));
                Region regView = new Region(path);
                Region regAll = new Region(pictureBox1.ClientRectangle);
                regAll.Xor(regView);
                g.FillRegion(Brushes.Black, regAll);
            }
        }

        private void UpdateStatusLabel()
        {
            switch (gameState)
            {
                case GameState.None:
                    status_label.Text = "Are you ready?";
                    break;
                case GameState.Hover:
                    status_label.Text = "Hover over any red square";
                    break;
                case GameState.Waiting:
                    status_label.Text = $"Hold cursor {time.Seconds} sec";
                    break;
                case GameState.FirstPart:
                    status_label.Text = $"{time.Minutes} : {time.Seconds.ToString("00")}";
                    break;
                case GameState.SecondPart:
                    status_label.Text = $"{time.Minutes} : {time.Seconds.ToString("00")}";
                    break;
            }
        }

        private void StartHover()
        {
            button1.Text = "Stop";
            gameState = GameState.Hover;
            UpdateStatusLabel();
            finishCollisions.ForEach(i => i.IsEnabled = true);
            pictureBox1.Invalidate();
        }

        private void StartWaiting()
        {
            gameState = GameState.Waiting;
            time = new TimeSpan(0, 0, 3);
            UpdateStatusLabel();
            timer1.Start();
        }

        private void StartFirstPart()
        {
            button1.Enabled = mode_comboBox.Enabled = false;
            gameState = GameState.FirstPart;
            switch (gameMode)
            {
                case GameMode.Easy:
                    time = new TimeSpan(0, 1, 20);
                    viewRadius = 200;
                    break;
                case GameMode.Normal:
                    time = new TimeSpan(0, 0, 50);
                    viewRadius = 140;
                    break;
                case GameMode.Hard:
                    time = new TimeSpan(0, 0, 40);
                    viewRadius = 100;
                    break;
            }
            UpdateStatusLabel();
            timer1.Start();

            var coll = finishCollisions.Where(i => i != startCollision).ToList();
            var rand = new Random();
            var index = rand.Next(coll.Count());
            endCollision = coll.ElementAt(index);
            if(gameMode != GameMode.Hard)
            {
                coll.RemoveAt(index);
                index = rand.Next(coll.Count());
                bonusCollision = coll.ElementAt(index);
                bonusType = (GameBonusType)rand.Next(Enum.GetValues(typeof(GameBonusType)).Length);
            }

            finishCollisions.ForEach(i => i.IsEnabled = false);

            pictureBox1.Invalidate();
        }

        private void StartSecondPart()
        {
            gameState = GameState.SecondPart;
            switch (gameMode)
            {
                case GameMode.Easy:
                    time = new TimeSpan(0, 1, 0);
                    break;
                case GameMode.Normal:
                    time = new TimeSpan(0, 0, 50);
                    break;
                case GameMode.Hard:
                    time = new TimeSpan(0, 0, 40);
                    break;
            }
            UpdateStatusLabel();
            timer1.Start();

            var coll = finishCollisions.Where(i => i != endCollision);
            startCollision = coll.ElementAt(new Random().Next(coll.Count()));
            startCollision.IsEnabled = true;

            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            cursorPos = e.Location;
            if (gameState == GameState.None)
                return;

            switch (gameState)
            {
                case GameState.Hover:
                    foreach(var ob in finishCollisions)
                    {
                        if (e.X > ob.Bounds.X && e.Y > ob.Bounds.Y && e.X < ob.Bounds.X + ob.Bounds.Width && e.Y < ob.Bounds.Y + ob.Bounds.Height)
                        {
                            startCollision = ob;
                            StartWaiting();
                            break;
                        }
                    }
                    break;
                case GameState.Waiting:
                    if (startCollision == null)
                        return;
                    if (!(e.X > startCollision.Bounds.X && e.Y > startCollision.Bounds.Y && e.X < startCollision.Bounds.X + startCollision.Bounds.Width && e.Y < startCollision.Bounds.Y + startCollision.Bounds.Height))
                        StartHover();
                    break;
                case GameState.FirstPart:
                    if (endCollision != null && endCollision.IsHit(e.Location))
                        StartSecondPart();
                    if(bonusCollision != null && bonusCollision.IsHit(e.Location))
                    {
                        switch (bonusType)
                        {
                            case GameBonusType.Clock:
                                time += new TimeSpan(0, 0, 15);
                                break;
                            case GameBonusType.Light:
                                viewRadius += 40;
                                break;
                            case GameBonusType.Diamond:
                                diamonds_label.Text = $"Diamonds: {++order.DiamondsCount}";
                                UpdateResultLabels();
                                SaveOrder();
                                break;
                        }
                        bonusCollision = null;
                    }
                    break;
                case GameState.SecondPart:
                    if (startCollision != null && startCollision.IsHit(e.Location))
                        WinGame($"You have successfully completed this mini-game in {elapsedTime.TotalSeconds} seconds!");
                    break;
            }

            if ((gameState == GameState.SecondPart || gameState == GameState.FirstPart) && collisions.IsHit(e.Location))
            {
                GameOver("You've hit a wall! Be careful next time.");
            }
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(gameState == GameState.None)
            {
                StartHover();
            }
            else
            {
                ResetGame();
            }
            pictureBox1.Invalidate();
        }

        private void diamondIcn_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            DrawDiamond(e.Graphics, diamondIcn_pictureBox.ClientRectangle);
        }

        private void diamonds_label_Click(object sender, EventArgs e)
        {
            MessageBox.Show("When passing the first stage of the game, you can find a blue crystal.\nHover over it to add it to your collection.\nCompete with your friends who has the most crystals!", "About diamonds");
        }

        private void mode_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameMode = (GameMode)Enum.Parse(typeof(GameMode), (string)mode_comboBox.SelectedItem);
        }

        private void winsCountIcn_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            DrawMedal(e.Graphics, winsCountIcn_pictureBox.ClientRectangle);
        }

        private void bestTimeIcn_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            DrawClock(e.Graphics, bestTimeIcn_pictureBox.ClientRectangle);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var t = new TimeSpan(0, 0, 1);
            time -= t;
            if(gameState == GameState.FirstPart || gameState == GameState.SecondPart)
                elapsedTime += t;
            if (time.Minutes <= 0 && time.Seconds <= 0)
            {
                timer1.Stop();
                switch (gameState)
                {
                    case GameState.Waiting:
                        StartFirstPart();
                        break;
                    case GameState.FirstPart:
                        GameOver("The time to complete the first part of the mini-game is up.");
                        break;
                    case GameState.SecondPart:
                        GameOver("The time to complete the second part of the mini-game is up.");
                        break;
                }
                return;
            }
            UpdateStatusLabel();
        }
    }
}