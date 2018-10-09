* **Tree**
    * Def
    * 術語 
* **Tree表示方法**
    * 利用link list表示
      * 分析
    * 將Tree化成Binary Tree，再予以表示
    * child-sibling方法]
    * 括號法表示
* **Binary Tree**
    * Def
    * Binary Tree v.s Tree
    * BT の 3個基本定理
      * BT中第i level之最多node數 = 2^(i-1)
      * 高度k之BT，其最多node數=(2^k)-1 ⇔  最少node數 = k (only 1 child)
      * 非空BT，若leaf有n0個，Degree-2 node數有n2個，則n0=n2+1
* **BT的種類**
    * Skewed BT
      * left-skewed BT
      * right-skewed BT
    * Full BT
    * Complete BT
      * Complete BT 定理
    * Strict BT
* **BT表示方法**
    * 利用Array表示/儲存
      * 優點 & 缺點
    * 利用linked list表示
      * 優點 & 缺點
* **BT Traversal**
    * Stack
      * DLR : Preorder
      * LDR : Inorder
      * LRD : Postorder
    * Queue
      * Level-order Traversal
 * **BT Traversal四大題型**
     * 給BT，求Traversal order
     * 給BT的前序+中序或後序+中序，決定唯一BT
     * Recursive Traversal algo for Pre,In,Postfix
     * 遞迴追蹤algo之應用
       * Copy a BT
       * Equal(S,T)
       * Count a BT Node數
       * 求 BT Height
       * Swap a BT
       * 以BT表示Expression
       * 針對Expression BT寫出求值的recursive algo
       * 利用Tree做Sorting
         * Heap sort
         * 利用Search Tree作Sort 
           * m-way Search Tree(n>>2)
           * Binary Search Tree
 * **Binary Search Tree(BST)**
     * Def
     * 利用BST作Sort
     * Delete x in a BST
     * Search x in a BST
 * **Heap**
     * Def
     * 應用
     * Insert x in Heap
     * Delete x in Heap
     * Build Heap
       * Top-down
       * Bottom-up
         * Adjust(tree,i,n)
         * BuildHeap(tree,n)
* **Thead BT**
    * 緣由
    * Thread pointer指向何處?以中序為準
    * Data Structure設計
    * 基本操作
      * Insuc(x)  
        InPre(x)
      * 簡化的Inorder追蹤algo(不用Recursion，無須Stack)
      * 插入t node in a Tread BT
* **Tree化成BT**
* **Forest化BT**
* **n個nodes可以形成不同結構之BT數**
* **Disjoint Set定義、表示方式、union與Find運作**
    * Def
    * 應用
    * 表示方法 
      * linklist
      * array
    * union(i,j)   
      Find(x)
    * 三種組合
      * 任意的union(i,j)與simple-Find(x)
      * union-bu-Height與Simple-Find(x)之組合
      * union-by-Height與Find-with-path-compression之組合
    * 應用 : 給予等位關係配對資訊，求出等位集合
      
         
       
       
      
  
  
    
  
     
  
