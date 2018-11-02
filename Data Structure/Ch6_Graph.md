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
* Def 
  * (1) E = T + B　　T = tree edge B = back edge
  * (2) 從 B 中隨便挑一個邊給 T，必會形成 cycle
  * (3) 任意 2 點必有 1 條 `唯一` 的 path ( Connected )
   
## min Spanning Tree
* Def : 每個邊都有 cost (成本)值，在 G 的所有 Spanning Tree 中，具有最小成本總和
  * (1) min Spanning Tree 可能 ≧ 1 顆 (∵ 很多邊有相同 cost)
  * (2) 但是如果每邊 cost 都不同，那麼 min Spanning Tree 只有 1 顆
* 求法
  * Kruskal's algo
  * Prim's algo
  * Sollin's algo
### 1. Kruskal's algo
#### (一) 方法
Step 1 : 從所有邊 E 挑出 cost 最小的邊  
Step 2 : 判斷這個邊加到 S 中會不會有 cycle，會的話就放棄這個邊 (一開始 S 是空的，慢慢加邊進去)  
Step 3 : 重複 Step 1 ~ Step 2 直到挑出 n-1 個邊 or 所有邊 E 都被挑完了  
Step 4 : 如果 S 的邊數 < n-1 -> 那 S 就不是 min Spannning Tree  
#### (二) Time 分析
Kruskal's algo 最多做 E 回合，而每回合主要做 2 個任務  
(1) Delete-min in cost edge : 利用 heap 維持成本值，則 delete-min 花 O(lgE)  
(2) 判斷邊加入 S 是否有 cycle  

### 2. Prim's algo
### 3. Sollin's algo
## Shortest path length problem 
* Dijkstra's algo 
* Bellman-Ford algo
* Floyd-Warshall algo
## Johnson algo
 


