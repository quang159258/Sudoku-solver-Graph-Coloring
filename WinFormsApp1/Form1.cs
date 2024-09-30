using WinFormsApp1.Model;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<TextBox> txtBox;
        Graph G;
        int[,] grid = new int[9, 9];
        private void Form1_Load(object sender, EventArgs e)
        {

            txtBox = this.flowLayoutPanel1.Controls.OfType<TextBox>().ToList();

            for (var i = 0; i < txtBox.Count; i++)
            {
                txtBox[i].TextAlign = HorizontalAlignment.Center;
                int row = i / 9;
                int col = i % 9;
                if (int.TryParse(txtBox[i].Text, out int number))
                {
                    grid[row, col] = number;
                }
                else
                {
                    grid[row, col] = 0;

                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            G = new Graph(grid);
            bool success = G.BacktrackingColoring();
            for (var i = 0; i < 81; i++)
            {
                txtBox[i].BackColor = G.Nodes[i].Color;
                txtBox[i].Text = G.Nodes[i].Number.ToString();
            }
            if (success)
            {
                label3.Text = G.loopCount.ToString(); ;
                label4.Text = G.recursionCount.ToString(); ;
                MessageBox.Show("Sudoku solved!");
            }
            else
            {
                MessageBox.Show("Failed to solve Sudoku.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sudoku s = new Sudoku();
            s.GenerateSudoku();
            grid = s.sudokuGrid;
            for (var i = 0; i < 81; i++)
            {
                int row = i / 9;
                int col = i % 9;
                var num = s.sudokuGrid[row, col];
                txtBox[i].Text = num.ToString();
                txtBox[i].BackColor = num == 0 ? Color.White:Graph.colors[num-1];
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
