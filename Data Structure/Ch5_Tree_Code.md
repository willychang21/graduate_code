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
	if(S->Data == T->Data)                             //比D                         
	{
		if(Equal(S->Leftchild,T->Leftchildtchild)      //比L
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
    nR = Count(T->Rightchild);	     //nL=Count(R)
	return (nL+nR+1);            //左子樹+右子樹+Root
}
```
    
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  

