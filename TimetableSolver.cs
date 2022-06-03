namespace TimetableBacktracking
{
    internal class TimetableSolver
    {
        private readonly bool[,] _timetable;
        private readonly int _rowCount;
        private readonly int _columnCount;
        private List<List<int>> _ones = new();
        private List<int[]> _solutions = new();

        public TimetableSolver(bool[,] timetable)
        {
            _timetable = timetable;
            _rowCount = timetable.GetLength(0);
            _columnCount = timetable.GetLength(1);
        }

        /// <summary>
        /// Tries to solve timetable problem.
        /// </summary>
        /// <returns>
        /// Returns list of all possible solutions.
        /// </returns>
        public List<int[]> Solve()
        {
            _solutions.Clear();
            try
            {
                _ones = FindOnes();
            }
            catch
            {
                return _solutions;
            }

            // default values are false, so no column has a one at the beggining
            bool[] isColumnChosen = new bool[_columnCount];
            // indexes chosen from ones in rows
            int[] chosenIndexes = new int[_rowCount];
            for (int i = 0; i < chosenIndexes.Length; ++i)
            {
                // -1 when no index was chosen for a row
                chosenIndexes[i] = -1;
            }

            SolveRecursive(0, chosenIndexes, isColumnChosen);

            return _solutions;
        }

        /// <summary>
        /// Finds indexes of true/one in a <see cref="_timetable"/>.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When there isn't a possible solution to a <see cref="_timetable"/></exception>
        private List<List<int>> FindOnes()
        {
            var list = new List<List<int>>();

            for (int i = 0; i < _rowCount; ++i)
            {
                list.Add(new List<int>());
                for (int j = 0; j < _columnCount; ++j)
                {
                    if (_timetable[i, j])
                    {
                        // add index of one
                        list[i].Add(j);
                    }
                }

                bool areAnyOnesInRow = list[i].Count > 0;
                if (!areAnyOnesInRow)
                {
                    throw new ArgumentException($"Timetable is missing ones on row {i}");
                }
            }

            return list;
        }

        // we only entry the function if the solution is reasonable
        // table types are passed through reference, int through value
        private void SolveRecursive(int row, int[] chosenIndexes, bool[] isColumnChosen)
        {
            if (row == _rowCount)
            {
                // solution found, we need to copy it since tables are modified
                var solutionArray = new int[_rowCount];
                chosenIndexes.CopyTo(solutionArray, 0);
                _solutions.Add(solutionArray);
                return;
            }

            var currentRowOnes = _ones[row];

            foreach (int oneIndex in currentRowOnes)
            {
                if (!isColumnChosen[oneIndex])
                {
                    // if the column has no one in solution we chose it
                    isColumnChosen[oneIndex] = true;
                    chosenIndexes[row] = oneIndex;

                    // we go into deeper row
                    SolveRecursive(row + 1, chosenIndexes, isColumnChosen);

                    // we clean up
                    isColumnChosen[oneIndex] = false;
                    chosenIndexes[row] = -1;
                }
            }
        }
    }
}
