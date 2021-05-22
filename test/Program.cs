using static System.Console;

using Cairo;
using Gdk;
using Gtk;

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
        
        if (changed != null)
            changed();
        return true;
    }
}

class View : DrawingArea {
    Board board = new Board();

    const int SQUARE = 100, MARGIN = 50;

    public View() {
        AddEvents((int) EventMask.ButtonPressMask);    // ask to receive button press events
    }

    // map from board coordinates to pixel coordinates
    // (0, 0) -> (MARGIN, MARGIN)
    // (1, 1) -> (MARGIN + SQUARE, MARGIN + SQUARE)
    // (2, 2) -> (MARGIN + 2 * SQUARE, MARGIN + 2 * SQUARE)
    PointD map(int x, int y) =>
        new PointD(MARGIN + x * SQUARE, MARGIN + y * SQUARE);

    void line(Context c, int from_x, int from_y, int to_x, int to_y) {
        c.MoveTo(map(from_x, from_y));
        c.LineTo(map(to_x, to_y));
        c.Stroke();
    }

    protected override bool OnDrawn(Context c) {
        // draw black background
        c.SetSourceRGB(0.0, 0.0, 0.0);
        c.Rectangle(0, 0, Allocation.Width, Allocation.Height);
        c.Fill();

        c.SetSourceRGB(1.0, 0.0, 0.0);  // red
        c.LineWidth = 10;

        for (int x = 1 ; x <= 2 ; ++x) {
            line(c, x, 0, x, 3);
            line(c, 0, x, 3, x);
        }

        return true;
    }

    protected override bool OnButtonPressEvent(EventButton e) {
        int x = ((int) (e.X - MARGIN)) / SQUARE;
        int y = ((int) (e.Y - MARGIN)) / SQUARE;
        WriteLine($"user clicked square {x}, {y}");

        return true;
    }
    
}

class TicTacToe : Gtk.Window {
    public TicTacToe() : base("tic tac toe") {
        Resize(400, 400);
        Add(new View());
    }

    protected override bool OnDeleteEvent(Event ev) {
        Application.Quit();
        return true;
     }

    static void Main() {
        Application.Init();
        new TicTacToe().ShowAll();
        Application.Run();
    }
}
