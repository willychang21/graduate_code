### 7.1
* Tree∶ acyaclic + connected + undirected
  * Tree 的特性
    * Bipartite (不含奇數長度cycle)
    * 每邊皆bridge
    * 最少邊connected graph
    * ⇔ acyaclic 且 | E | = | V | - 1
    * ⇔ connected 且 | E | = | V | - 1
* Trival Tree (degenerated tree)∶ one point tree ( | V | = 1 )
* Nontrival Tree∶ | V | ≥ 2 
  * ⇔ 任二點恰一條path相連 ( acyaclic,connected,| E | = | V | -1 )
  * ⇔ connected 去一邊變 disconnectd 
  * ⇔ acyaclic 加一邊恰含 1 cycle
  * ⇒ 至少2 個 pendant 
* Pandent∶ dev(v) = 1 ,v 為 pandent
* Forest∶ acyaclic + undirected ( | E | = | V | - K(G) )

### 7.2 
* directed tree∶ 視為無向為tree
* root tree∶ ∃! r∈ V 使得 id(r)=0
* m-ary tree∶ internal 至多 m 個 son
  * T( V,E ) = m-ary tree ,n = | V | ,l = leaf 個數 ,i = internal node 數
  * n ≤ m_i  + 1
  * n = l + i
  * l + i ≤ m_i  + 1 ⇒  ( m - 1) i  ≥  l – 1  ⇒  i ≥(l-1)/(m-1)  
* binary tree∶ m=2
* full m-ary tree∶ 恰含 m 個 son 
  * T( V,E ) = m-ary tree ,n = | V | ,l = leaf 個數 ,i = internal node 數
  * n= m_i+1
  * full m-ary tree with height h
    * (m-1)(h-1)+m≤l≤m^h
    * mh+1≤n≤(m^(h+1)-1)/(m-1)
    * ⇒l ≤ m^h   ⇒h ≥ ⌈〖log〗_m⁡l ⌉
  * T∶ full binary tree 
    * E=I+2i  
      * ( E∶external path length ,I∶internal path length ,i∶node 數 )
  * E=( m-1)I+mi  (full m-ary tree) 
  * complete m-ary  tree∶ full m-ary tree + 所有的leaf相同level
* balanced tree∶ 所有的leaf 具 level h or h-1

### 7.3
* Spanning Tree∶ G(V,E):  connected  
若T為G之spanning subgraph，且T為tree ，稱T為G之一Spanning Tree (S.T)
  * G 含 S.T ⇔ G∶ connected
* G∶ connected planner ⇒ v-e+r=2
* 求G之S.T個數∶ 暴力法
* Matrix Tree
  * V={ v_(1~) v_n}  ,M =[ m_ij]∶ n×n 
  * m_ij={█(deg⁡(v_i ),if i=j@-1,if i=j"且" v_i "與" v_j "相連" @       0,if i=j"且" v_i "與" v_j "不相連" )┤
  * N(G) = M 之各cofactor
* G:connected   ,   T:S.T  ,C:cycle ,E:cut set
  * E & T，至少含一個共同邊
  * ¯(T )& C，至少含一個共同邊
  * E & C ，含偶數個共同邊
* Cayley number
  * N(k_n )=n^(n-2)
  * N(k_(m,n) )=m^(n-1)∙n^(n-1)
### 7.4
* Weight graph∶ w:E→R weight founction ，"稱" G( V,E,W )"為" weight graph
  * minimum spanning tree (MST )  ": " weight graph+ connected  
 "且" G "之" S.T "中具邊之" Weight"總和最小者"   
"稱為" G"之" MST
  * MST"未必唯一" 
  * 當所有邊的weight都相異，MST唯一(有相同也可能唯一→檢查能否替換)
* Kruskal algorithm
  * 由邊開始
  * Greedy algorithm
  * Keep acyclic
  * Complexity∶ O(mlog⁡m ),m=|E|
  * acyclic,|E|=|V|-1⇔tree ,O(E log⁡E)
* Prim’s algorithm
  * 由點開始
  * Greedy algorithm
  * Keep connected
  * Complexity∶ O(n^2 ),n=|V|
  * connected,|E|=|V|-1 
  * "若用" Link List,Binary Heap ,O(V log⁡V+E log⁡V)
### 7.5
* Encode 
  * 固定長
  * 變動長
* Prefix code∶G:set of codeword  
"若"∀x≠y∈G,x"不為" y"之" prifix,"稱" G"為" prefix code
