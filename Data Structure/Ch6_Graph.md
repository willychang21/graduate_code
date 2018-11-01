# Graph
## Graph
* Def
* 相關術語
## Graph 表示方式
* Adjacency Matrix
* Adjacency List
* Adjacency Multilists
* Incidence Matrix
### 1. Adjacency Matrix
* Def
* 無向圖 (Symmetric)
  * Q1 : 判斷(i,j)是否存在 : O(1)
  ```C#
  if(A[i,j])
    "存在"
  else
    "不存在"  
  ```
  * Q2 : 求頂點 i 之 Degree : O(n)
  ```C#
  for(j=i;j<=n;j++)
  {
    sum = sum + A[i,j];
  }
  ```
  * Q3 : 求 Graph 邊數 : O(n^2)
  ```C#
  for(i=1;i<=n;i++)  //所有元素加總
  {
    for(j=1;j<=n;j++)
    {
      E = E + A[i,j];
    }
  } 
  E = E / 2;  //Graph 邊數 = 所有元素加總 / 2
* 有向圖 (不一定 Symmetric)
  * Q1 : 求頂點之 i 之 in/out Degree : O(n)
   ```C#
  for(j=1;j<=n;j++)
  {
    in = out + A[i,j];  //第 i 列 sum
  }
   for(i=1;i<=n;i++)
  {
    out = out + A[i,j]; //第 i 行 sum
  }
  ```
  * Q2 : 求 Graph 邊數 : O(n^2) 
  ```C#
   for(i=1;i<=n;i++)  //所有元素加總
  {
    for(j=1;j<=n;j++)
    {
      E = E + A[i,j];
    }
  } 
  ``` 
### 2. Adjacency List
* Def
* 無向圖
  * e = 1/2( Vi 串列長度總和 = 所有串列 node 總數 )
  * Q1 : 判斷邊 (i,j) 是否存在 : O(e)
  * Q2 : 求頂點 i 之 Degree
  * Q3 : 求 Graph 邊數 : O(n+e)
  ```C#
  Sum = 0;
  for(i=1;i<=n;i++)  //n or n+1 次(含失敗)
  {
    P = vertex[i];   
    while(P != Nil)  //2e 次 : 所有 node
    {
      Sum = Sum + 1;
      P = P -> Next;
    }
  }
  return Sum/2;
* 有向圖
  * Q1 : 求頂點之 i 之 in/out Degree
    * in : 所有串列的 i 出現次數 : O(n+e)
    * out : Vi 串列長度 ≦ O(e)  
  * Q2 : 求 Graph 邊數 : O(n+e)

|                                       | Adjacency Matrix | Adjacency List |
|---------------------------------------|------------------|----------------|
| 圖形 V 很多，但是 E 很少              | 不適合           | 適合           |
| 圖形 V 很少，但是 E 很多              | 適合             | 不適合         |
| 經常判斷 E 存在                       | √ O(1)           | x time ≦ O(e)  |
| 求圖形邊數、 判斷連通、 判斷有無cycle | x O(n^2)         | √ O(n+e)       |

## Graph Traversal
* DFS [DS]
  * DFS order 不唯一
  * 通常 vertex no. 小優先
* DFS [Algo] [有向]
* BFS
  * BFS order 也不唯一
* BFS [Algo] [無向]
* 應用

## Spanning Tree
## min Spanning Tree
## Kruskal's algo
## Prim's algo
## Sollin's algo
## Shortest path length problem 
* Dijkstra's algo 
* Bellman-Ford algo
* Floyd-Warshall algo
## Johnson algo
 


