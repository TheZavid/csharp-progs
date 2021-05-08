using static System.Console;

using System.Drawing;
using System.Windows.Forms;

delegate void Notify();

// A model class for a game of Tic Tac Toe.
class Board {
    int[,] square = new int[3, 3];    // 0 = empty, 1 = player 1, 2 = player 2

    public int player = 1;     // whose turn it is
    public int winner = 0;     // who has won
    public int win_x, win_y, win_dx, win_dy;    // vector indicating winning squares

    public event Notify changed;    // fires whenever the board changes

    public int this[int x, int y] {
        get => square[x, y];
    }

    // Return true if the current player has three in a row,
    // starting at (x, y) and moving in direction (dx, dy).
    bool all(int x, int y, int dx, int dy) {
        for (int i = 0 ; i < 3 ; ++i)
            if (square[x + i * dx, y + i * dy] != player)
                return false;

        win_x = x; win_y = y; win_dx = dx; win_dy = dy;
        return true;
    }

    // Return true if the current player has won.
    bool checkWin() {
        // check rows and columns
        for (int i = 0 ; i < 3 ; ++i)
            if (all(i, 0, 0, 1) || all(0, i, 1, 0))
                return true;

        // check diagonals
        return all(0, 0, 1, 1) || all(2, 0, -1, 1);
    }

    // Make the current player play at (x, y).    Return true if the move was legal.
    public bool move(int x, int y) {
        if (square[x, y] > 0)    // square is occupied
            return false;

        square[x, y] = player;
        if (checkWin())
            winner = player;
        else player = 3 - player;

        changed();
        return true;
    }
}

class MyWindow : Form {
    Board board = new Board();

    Pen pen = new Pen(Color.Red, 10);

    const int SQUARE = 100, MARGIN = 50;

    public MyWindow() {
        Text = "tic tac toe";
        ClientSize = new Size(400, 400);
        BackColor = Color.Black;
        StartPosition = FormStartPosition.CenterScreen;
    }

    // map from board coordinates to pixel coordinates
    // (0, 0) -> (MARGIN, MARGIN)
    // (1, 1) -> (MARGIN + SQUARE, MARGIN + SQUARE)
    // (2, 2) -> (MARGIN + 2 * SQUARE, MARGIN + 2 * SQUARE)
    Point map(int x, int y) =>
        new Point(MARGIN + x * SQUARE, MARGIN + y * SQUARE);

    void line(Graphics g, int from_x, int from_y, int to_x, int to_y) {
        g.DrawLine(pen, map(from_x, from_y), map(to_x, to_y));
    }
    void Draw_Player(Graphics g, int player, int x, int y) {
            if (player == 1)
                g.DrawEllipse(pen, map(x, y), SQUARE, SQUARE);
            else {
                    (int nx, int ny) = map(x, y);
                    line(g, nx, ny, map(nx + 1, ny + 1));
            }
    }

    protected override void OnPaint(PaintEventArgs args) {
        Graphics g = args.Graphics;

        for (int x = 1 ; x <= 2 ; ++x) {
            line(g, x, 0, x, 3);
            line(g, 0, x, 3, x);
        }

        for (int y = 0; y <= 2; y++) {
                for (int x = 0; x <= 2; x++) {
                        if (board.square[x, y] > 0)
                                Draw_Player(g, board.square[x, y], x, y);
                }
        }
    }

    protected override void OnMouseDown(MouseEventArgs args) {
        int x = ((int) (args.X - MARGIN)) / SQUARE;
        int y = ((int) (args.Y - MARGIN)) / SQUARE;
        WriteLine($"user clicked square {x}, {y}");
        if ((x <=2 && x >= 0) && (y <=2 && y >= 0)) {
                board.move(x, y);
        }
    }
}

class Hello {
    static void Main() {
        Application.Run(new MyWindow());
    }
}
