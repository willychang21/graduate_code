### Preorder
```C++
void BinaryTree::Preorder(TreeNode *T)
{
    if (T != NULL) 
    {                          
        std::cout << T->Data  ;       // D
        Preorder(T->leftchild);       // L
        Preorder(T->rightchild);      // R
    }
}
```
### Inorder
```C++
void BinaryTree::Inorder(TreeNode *T)
{
    if (T != NULL) 
    {           
        Inorder(T->leftchild);        // L
        std::cout << T->Data ;        // D
        Inorder(T->rightchild);       // R
    }
}
```
### Postorder
```C++
void BinaryTree::Postorder(TreeNode *T)
{
    if (T != NULL) 
    {                   
        Postorder(T->leftchild);      // L
        Postorder(T->rightchild);     // R
        std::cout << T->Data;         // D
    }
}
```
### Levelorder
```C++
void BinaryTree::Levelorder()
{    
    std::queue<TreeNode*> q;                // 把root作為level-order traversal之起點
    q.push(this->root);					    // 推進root中
    
    while (!q.empty())                      // 若queue不是空的, 表示還有node沒有visiting
    {                    
    	TreeNode *T = q.front();       // 取出先進入queue的node
    	q.pop();                          
    	std::cout << T->Data;          // 進行visiting

    	if (T->leftchild != NULL)      // 若leftchild有資料, 將其推進queue
        {    
            q.push(T->leftchild);
        }
        if (T->rightchild != NULL)     // 若rightchild有資料, 將其推進queue
        {   
            q.push(T->rightchild);
        }
    }
}
```
### Copy a B.T  : Time O(n)
```C++
void Copy(TreeNode *Origin)
{
    if (Origin == NULL)
    {
      t = Null;
    }
    else
    {                   
      new(t);
      t -> Data = Origin -> Data ;
      t -> leftchild = Copy(Origin->leftchild);
      t -> Rightchild = Copy(Origin->Righttchild);      
    }
    return t;
}
```
### Equal(S,T) 利用 preorder(效率比中後序高)
```C++
void Equal(TreeNode *S,TreeNode *T)
{
  result = False;                                      //設初始值為False
  if(S==NULL && T==NULL)  
  {
    result = True;                                     //兩個都空的 -> 一樣
  }
  else if(S!=NULL && T!=NULL)
  {
	if(S->Data == T->Data)                                     //比D                         
	{
		if(Equal(S->Leftchild,T->Leftchildtchild)          //比L
		{ 
			result=Equal(S->Rightchild,T->Rightchild); //比R
		}
	}
  }
  else
    result = False;
}
```
### Count B.T Node num 利用 postorder
```C++
void Count(TreeNode *T)
{
  if(T==NULL)
  {
	return 0;
  }
  else
  {
	nL = Count(T->Leftchild);    //nL=Count(L)
        nR = Count(T->Rightchild);   //nL=Count(R)
	return (nL+nR+1);            //左子樹+右子樹+Root
}
```

### B.T Height
```C++
void Hight(TreeNode *T)
{
  if(T==NULL)
  {
	  return 0;	  
  }
  else
  {
	  HL = Hight(T->Leftchild);  //HL=Hight(L)
	  HR = Hight(T->Rightchild); //HR=Hight(R)
	  return MAX(HL,HR)+ 1;      //左子樹+右子樹+Root
  }
}
```
### Swap B.T  利用 postorder
```C++
void Swap(TreeNode *T)  
{
  if(T!=NULL)
  {
	Swap(T->Leftchild);
	Swap(T->Rightchild);
	temp = T->Leftchild;
	T->Leftchild = T->Rightchild;
	T->Rightchild = temp;
  }
}
```
### Expression B.T 求值 Recursive algo
```C++
void Evaluate(TreeNode *T)     //T:expression B.T
{
  if(T!=NULL)
  {
	  Evaluate(T->Leftchild);
	  Evaluate(T->Rightchild);
	  switch(T->Data)          //Data 放 operator 
	  {
	      case "+" : T->res = (T->Leftchild)->res + (T->Rightchild)->res; // res 放 operand
	      case "-" : T->res = (T->Leftchild)->res - (T->Rightchild)->res;
	      case "*" : T->res = (T->Leftchild)->res * (T->Rightchild)->res;
	      case "/" : T->res = (T->Leftchild)->res / (T->Rightchild)->res;
	      case "~" : T->res = ~(T->Rightchild)->res;
              case "變數名" : T->res= 變數值 //常數值
	  }
  }
}
```

### BST Sort
```C++
void BST::InorderSort(){
    TreeNode *T = new TreeNode;
    T = Leftmost(root);  //找到Binary Tree整棵樹中「最左」的node
    while(T){
        cout << T->element << "(" << T->key << ")"; //element:名字悟空;key:戰鬥力指數5000
        T = Successor(T); //找到「下一個」
    }
}
```
### BST Insert x
Time : O(lgn)
```C++
void BST::InsertBST(int key, string element)
{

    TreeNode *y = 0;        // 準新手爸媽
    TreeNode *x = 0;        // 哨兵
    TreeNode *insert_node = new TreeNode(key, element);   // insert_node為將要新增的node

    x = root;
    while (x != NULL)                   // 在while中, 以如同Search()的方式尋找適當的位置  
    {                      
        y = x;                          // y先更新到原來x的位置
        if (insert_node->key < x->key)  // 判斷x是要往left- 還是right- 前進
	{ 
            x = x->leftchild;
        }
        else
	{
            x = x->rightchild;
        }
    }                                   // 跳出迴圈後, x即為NULL
                                        // y即為insert_node的parent
    insert_node->parent = y;            // 將insert_node的parent pointer指向y

    if (y == NULL)                      // 下面一組if-else, 把insert_node接上BST
    {                     
        this->root = insert_node;
    }
    else if (insert_node->key < y->key)
    {
        y->leftchild = insert_node;
    }
    else
    {
        y->rightchild = insert_node;
    }
}
```
### BST Delete x
Time : O(lgn)
```C++
void BST::DeleteBST(int KEY){               // 要刪除具有KEY的node

    TreeNode *delete_node = Search(KEY);    // 先確認BST中是否有具有KEY的node
    if (delete_node == NULL) {
        std::cout << "data not found.\n";
        return;
    }

    TreeNode *y = 0;      // 真正要被刪除並釋放記憶體的node
    TreeNode *x = 0;      // 要被刪除的node的"child"

    if (delete_node->leftchild == NULL || delete_node->rightchild == NULL){
        y = delete_node;
    }
    else{
        y = Successor(delete_node);        // 將y設成delete_node的Successor
    }                                      // 經過這組if-else, y調整成至多只有一個child
                                           // 全部調整成case1或case2來處理
    if (y->leftchild != NULL){
        x = y->leftchild;                  // 將x設成y的child, 可能是有效記憶體,
    }                                      // 也有可能是NULL
    else{
        x = y->rightchild;
    }

    if (x != NULL){                        // 在y被刪除之前, 這個步驟把x接回BST
        x->parent = y->parent;             // 此即為圖二(c)中, 把基紐接回龜仙人的步驟
    }
                                           // 接著再把要被釋放記憶體的node之"parent"指向新的child
    if (y->parent == NULL){                // 若刪除的是原先的root, 就把x當成新的root 
        this->root = x;
    }
    else if (y == y->parent->leftchild){    // 若y原本是其parent之left child
        y->parent->leftchild = x;           // 便把x皆在y的parent的left child, 取代y
    }
    else{                                   // 若y原本是其parent之right child
        y->parent->rightchild = x;          // 便把x皆在y的parent的right child, 取代y
    }                                       
                                            // 針對case3
    if (y != delete_node) {                 // 若y是delete_node的替身, 最後要再將y的資料
        delete_node->key = y->key;          // 放回delete_node的記憶體位置, 並將y的記憶體位置釋放
        delete_node->element = y->element;  // 圖二(d), y即是達爾, delete_node即是西魯
    }

    delete y;                               // 將y的記憶體位置釋放
    y = 0;
}
```
## BST Search x  
Worst case = O(n)  //Skewed BST 
Best case = O(lgn) //Full BST  
Avg case = O(n)
```C++
void BST::Search(int KEY){

    TreeNode *T = root;                // 將curent指向root作為traversal起點

    while (T != NULL && KEY != T->key) // 兩種情況跳出迴圈:1.沒找到 2.有找到
    {              
                                             
        if (KEY < T->key)
	{                      
            T = T->leftchild;          // 向左走
        }
        else 
	{
            T = T->rightchild;         // 向右走
        }
    }
    return T;
}
```
### AdjustHeap & BuildMaxHeap
```C++
void AdjustHeap(tree,i,n)
{
	//tree : array[1...n] of int ;
	//調整以 i 為 root 之 subtree 成為 Heap ;
	x = tree[i];    //保存
	j = 2*i;        //取得left child
	while(j <= n)do //成立表示還有left child
	{
	   if(j < n)                   //有right child
	       if(tree[j] < tree[j+1]) //找到child之MAX
		   j = j + 1;          //J表示MAZ{lchild,Rchild}之index
	   if(x >= tree[j])
	       break;
	   else
	   {
		tree[j/2]=tree[j];//上移到parent
		j=2*j;            //新的lchild位置
	   }
	}//while
	tree[j/2]=x //x置入正確底
   
}
void BuildMaxHeap(tree,n)
{
    for(i<=n;i>=1;i--)
	AdjustHeap(tree,i,n);		
}
```



    
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  

