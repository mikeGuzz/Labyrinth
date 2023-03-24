namespace Labyrinth
{
    public partial class Form1 : Form
    {
        private bool up, down, left, right;
        private int speed = 8;

        private List<Rectangle> collisions;

        private Rectangle player;

        private Pen playerPen; 
        private Pen collisionPen;

        public Form1()
        {
            InitializeComponent();

            player = new Rectangle(120, 120, 20, 20);
            playerPen = new Pen(Color.DarkRed, 3);
            collisionPen = new Pen(Color.DarkBlue, 3);

            collisions = new List<Rectangle>()
            {
                new Rectangle(200, 100, 30, 120)
            };

            timer1.Interval = 20;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int xO = 0, yO = 0;
            if (left && player.Left > ClientRectangle.Left)
                xO = -1;
            if (right && player.Right < ClientRectangle.Right)
                xO = 1;
            if (up && player.Top > ClientRectangle.Top)
                yO = -1;
            if (down && player.Bottom < ClientRectangle.Bottom)
                yO = 1;
            var pos = new Point(player.X + (speed * xO), player.Y + (speed * yO));
            player.Location = pos;

            foreach(var ob in collisions)
            {

            }

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.DrawRectangle(playerPen, player);

            collisions.ForEach(i => g.DrawRectangle(collisionPen, i));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                up = true;
            if (e.KeyCode == Keys.A)
                left = true;
            if (e.KeyCode == Keys.S)
                down = true;
            if (e.KeyCode == Keys.D)
                right = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                up = false;
            if (e.KeyCode == Keys.A)
                left = false;
            if (e.KeyCode == Keys.S)
                down = false;
            if (e.KeyCode == Keys.D)
                right = false;
        }
    }
}