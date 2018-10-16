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

