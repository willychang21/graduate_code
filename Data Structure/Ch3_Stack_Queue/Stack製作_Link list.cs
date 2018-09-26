#include <stdio.h>
#include <stdlib.h>
 
struct Node{
	int data; //堆疊資料的宣告 
	struct Node *next;  //堆疊中用來指向下一個節點 
};
typedef struct Node Stack_Node; //定義堆疊中節點的新形態
typedef Stack_Node *Linked_Stack;  //定義串列堆疊的新形態
Linked_Stack top=NULL; //指向堆疊頂端的指標 
int isEmpty();
void push(int); 
int pop();
 
int main(int argc, char *argv[]) {
	int value;
	int i;
	printf("請依序輸入10筆資料:\n");
	for(i=0;i<10;i++){
		scanf("%d",&value);
		push(value);
	}
	printf("====================\n");
	while(!isEmpty()){
		printf("堆疊彈出的順序為:%d\n",pop()); 
	}
	pop();
	return 0;
}
/*判斷是否為空堆疊*/
int isEmpty(){
	if(top==NULL){
		return 1; 
	}else{
		return 0;
	}
} 
/*將指定的資料存入堆疊*/
void push(int data){
	Linked_Stack new_add_node;  //新加入節點的指標
	/*配置新節點的記憶體*/
	new_add_node=(Linked_Stack)malloc(sizeof(Stack_Node));
	new_add_node->data=data;  //將傳入的值設為節點的內容 
	new_add_node->next=top;   //將新節點指向堆疊的點端 
	top=new_add_node;  //新節點成為堆疊的頂端 
} 
/*從堆疊取出資料*/
int pop(){
	Linked_Stack ptr;  //指向堆疊頂端的指標
	int temp;
	if(isEmpty()){
		printf("堆疊為空\n");
		return -1;
	}else{
		ptr=top;  //指向堆疊的頂端
		temp=ptr->data; //取出堆疊資料
		top=top->next; //將堆疊頂端的指標指向下一個節點 
		free(ptr); //將節點占用的記憶體釋放
		return temp; 
	}
}
