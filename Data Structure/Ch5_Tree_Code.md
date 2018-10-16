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
