Console.WriteLine("Hello, Łukasz!");

bool I = true;
bool O = false;

var problem = new bool[,]
{
    { O, O, I, I, O },
    { I, O, O, O, O },
    { O, I, I, O, O },
    { O, O, O, I, I },
    { I, O, O, O, I },
};

var solver = new TimetableBacktracking.TimetableSolver(problem);

var solutions = solver.Solve();

Console.WriteLine("Solutions:");
foreach (var solution in solutions)
{
    for (int i = 0; i < problem.GetLength(0); ++i)
    {
        for (int j = 0; j < problem.GetLength(1); ++j)
        {
            if (solution[i] == j)
                Console.Write("I ");
            else
                Console.Write("O ");
        }
        Console.WriteLine();
    }
}
