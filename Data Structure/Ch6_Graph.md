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
(2) 判斷邊加入 S 是否有 cycle : O(1)
作法 : 利用 Disjoint sets 的 Union 與 Find 運作，Initially，每個點都當一個 Disjoint Set  
⇒ 採用 Union-by-Height & Find-with-path-Compression
```C#
if(Find(u) != Find(v))  //Find(x) ⇒ O(α(m,n)) ≒ O(lg*n) ⇒ O(1)
{
  add(u,v) into S;
  Union(u,v);          //Union(i,j) ⇒ O(1)
}
else
{
  give up (u,v) edge;
}
```
(3) Kruskal's Time = E * O(lgE) = O(ElgE)  
#### [補充] Algo版
Kruskal = O(ElgE)  
又 E < v^2 ⇒ lgE < lgv^2 = 2lgv  
所以 O(ElgE) = O(Elgv)

### 2. Prim's algo
* Prim's algo [DS]
* Prim's algo [Algo]
#### Prim's algo [DS]
G(V,E) , V = {1,2,3...n} , 令 U = {1}  
Step 1 : 挑出最小成本邊 (u,v)，其中 u ∈ U , v ∈ V - U  
Step 2 : (u,v) 加入 S 中，且 v 從 U - V 移到 U  
Step 3 : 重複 step 1~2 直到 U = V or 無邊可挑  
Step 4 : if(S 的邊數 < V - 1) then 無 Spanning Tree 
**Time** : O(V^2) (用 Adjacency Matrix)
#### Prim's algo [Algo] -> Based on BFS
Time 分析 : O(V) + O(V) + O(VlgV) + O(ElgV) = O(ElgV) or O(VlgV) + O(ElgV)  
一般而言，E > V (E ≧ V-1)，所以 Prim's time = O(ElgV) = Kruskal's time
```C#
MST-Prim(G,w,r) //w : 個邊成本值集合，r : start vertex 起點
{
  for(each u ∈ G.V) //設初值，V 個頂點 ⇒ O(V)
  {
    u.Key = ∞;
    u.π = Nil;
  }
  r.Key = 0;  //起點 Key 值設 0
  Q = G.V;    //把每點 Key 值放入 Priority Queue : Q 中 ⇒ 即將每個點的 Key 值建立一個 min-Heap ⇒ O(V)
  while(Q != 0)
  {
    u = Extract-min(Q); //刪除最小 Key 值的點 ⇒ V 次 * 每次 O(lgV) ⇒ O(VlgV)
    for(each v ∈ G.Adj[u]) //共 check Adj.list 所有 Nodes (2E個) ⇒ loop 2E 回
    {
      if(v ∈ Q && w(u,v) < v.key) //比較小的話就更新 Key 值
      {
        v.π = u;
        △v.key = w(u,v);  //相當於是一個 Decrease-key 運作 ⇒ O(lgV)
      }
    } // for loop 共花了 : 2E * O(lgV) = O(ElgV)
  }
}
```
Q : 如何在更 fast ？    
A : Priority Queue 不要用 Binary-Heap，改用 Fibonacci Heap，其 Decrease-key 只需花 O(1) (in amortiged cost)     
所以 v.key = w(u,v) 此行花了 2E * O(1) = O(E)    
∴ Prim's time = O(VlgV) + O(E) = O(VlgV + E)

### 3. Sollin's algo
Initially,每個點視為獨立 tree 的 root  
Step 1 : 每顆 tree，挑出最小成本的樹邊  
Step 2 : 每顆都挑出來後，如果有重複的就刪掉
Step 3 : 重複 step 1~2，直到只剩 1 顆 tree or 無邊可挑
Step 4 : if(S 的邊數 < V - 1) then 無 Spanning Tree 
## Shortest path length problem 
* Dijkstra's algo 
* Bellman-Ford algo
* Floyd-Warshall algo  

|                 |           Dijkstra's algo          |          Bellman-Ford algo         | Floyd-Warshall algo |
|:---------------:|:----------------------------------:|:----------------------------------:|:-------------------:|
|     解決問題    | Single source to other Destination | Single source to other Destination | All pairs of vertex |
|   圖中可有負邊  |                  x                 |                  √                 |          √          |
| 可有負長度cycle |                  x                 |                  x                 |          x          |
|       策略      |               Greedy               |         Dynamic Programming        |   Dynamic Program   |
|   時間 : 矩陣   |               O(V^2)               |               O(V^3)               |        O(V^3)       |
|   時間 : 串列   |       O(ElgV) or O(VlgV + E)       |               O(V*E)               |                     |

### 1. Dijkstra's algo 

### 2. Bellman-Ford algo

### 3. Floyd-Warshall algo

## Johnson algo

## AOV Network & Topological Sort
## AOV Network & Critical Path & Critical Tasks
## Articulation point & Biconnected Graph & Biconnected Component
## 如何求出 Articulation point ？
 


