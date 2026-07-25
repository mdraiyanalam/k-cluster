\# K-Means Clustering for 3D Points (C# Sketch)



\## Overview



This is a clean, educational implementation of the \*\*K-Means clustering\*\* algorithm written in C#.



\*\*Task:\*\*  

Given an array of 3D points (arrays of 3 floating-point numbers), the algorithm returns the coordinates of the cluster centroids.



\## Algorithm



This implementation follows the classical \*\*Lloyd’s algorithm\*\*:



1\. \*\*Initialization\*\* – Randomly select \*k\* distinct points as initial centroids (Forgy method).

2\. \*\*Assignment\*\* – Assign every point to the nearest centroid using squared Euclidean distance.

3\. \*\*Update\*\* – Recalculate each centroid as the arithmetic mean of the points assigned to it.

4\. Repeat steps 2–3 until convergence or maximum iterations are reached.



\## Code Explanation



| Part                        | Purpose                                                                 |

|----------------------------|-------------------------------------------------------------------------|

| Random selection of \*k\* distinct points | Classic Forgy-style initialization (simple and sufficient for a sketch) |

| `DistanceSquared`          | Avoids the expensive square-root; the arg-min stays the same            |

| Assignment loop            | Each point is assigned to the nearest centroid                          |

| `changed` flag             | Early exit when no point changes its cluster (convergence)              |

| Sums + counts              | Efficient way to compute the new mean of each cluster                   |

| Empty-cluster handling     | Keeps the previous centroid (common practical choice)                   |

| Return value               | Exactly the list of cluster coordinates required by the task            |



\## Why This Solution is Correct



\- Follows the classical Lloyd algorithm described in all standard sources (Wikipedia, McCaffrey, StatQuest, etc.).

\- Input/output signature matches the requirement exactly.

\- Uses Euclidean distance (the mathematically correct metric for the usual WCSS objective).

\- Centroids are true arithmetic means.

\- Contains the usual practical safeguards (max iterations, convergence check, empty-cluster handling).

\- Based on a well-known educational implementation by James McCaffrey (Microsoft Research / Visual Studio Magazine).



\## References - See Other GitHub Branch



\- James McCaffrey – \*K-Means Data Clustering from Scratch Using C#\* (Visual Studio Magazine, Dec 2023)

\- Paulo Silva – \*Implementing K-Means Clustering From Scratch in JavaScript\* (Medium)

\- Wikipedia – K-means clustering

\- StatQuest – K-means clustering explanation



\## How to Run 



1\. Create a new \*\*Console App\*\* in Visual Studio.

2\. Replace the content of `Program.cs` with the provided code.

3\. Press \*\*F5\*\* to run.

