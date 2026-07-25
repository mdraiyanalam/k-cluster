using System;

namespace ThreeDArrayDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 3D array: [block][row][column]
            // Final state in the video: 3 blocks, 4 rows, 4 columns
            double[,,] threeDArray =
            {
                // ========== Block 0 ==========
                {
                    { 1.1, 1.2, 1.3, 1.4 },   // row 0
                    { 2.1, 2.2, 2.3, 2.4 },   // row 1
                    { 3.1, 3.2, 3.3, 3.4 },   // row 2
                    { 4.1, 4.2, 4.3, 4.4 }    // row 3
                },

                // ========== Block 1 ==========
                {
                    { 11.1, 11.2, 11.3, 11.4 },
                    { 12.1, 12.2, 12.3, 12.4 },
                    { 13.1, 13.2, 13.3, 13.4 },
                    { 14.1, 14.2, 14.3, 14.4 }
                },

                // ========== Block 2 ==========
                {
                    { 21.1, 21.2, 21.3, 21.4 },
                    { 22.1, 22.2, 22.3, 22.4 },
                    { 23.1, 23.2, 23.3, 23.4 },
                    { 24.1, 24.2, 24.3, 24.4 }
                }
            };

            // Get the sizes dynamically (exactly as shown in the video)
            int blockLength = threeDArray.GetLength(0);  // number of blocks
            int rowLength = threeDArray.GetLength(1);  // number of rows
            int colLength = threeDArray.GetLength(2);  // number of columns

            Console.WriteLine("=== 3D Array Contents ===\n");

            // Three nested loops – outer loop = blocks
            for (int block = 0; block < blockLength; block++)
            {
                Console.WriteLine($"----- Block {block} -----");

                for (int row = 0; row < rowLength; row++)
                {
                    for (int col = 0; col < colLength; col++)
                    {
                        Console.Write($"{threeDArray[block, row, col]}\t");
                    }
                    Console.WriteLine();   // new line after each row
                }
                Console.WriteLine();       // blank line between blocks
            }

            // Set a breakpoint on the next line (or just after Main)
            // then run in Debug mode → Debug → Windows → Array Visualizer
            // to see the graphical 3-D representation.
            Console.WriteLine("\nProgram finished. (Set breakpoint here for Array Visualizer)");
            Console.ReadKey();
        }
    }
}