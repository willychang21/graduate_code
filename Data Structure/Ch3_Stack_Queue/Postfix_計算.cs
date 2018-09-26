#include <stdio.h> 
#include <stdlib.h> 

#define MAX 80

void inToPostfix(char*, char*); // 中序轉後序 
int priority(char); // 運算子優先
double eval(char*); 
double cal(char, double, double); 

int main(void) { 
    char infix[MAX] = {'\0'}; 

    printf("運算式："); 
    scanf("%s", infix); 
    printf("%f", eval(infix));
    
    return 0; 
} 

void inToPostfix(char* infix, char* postfix) { 
    char stack[MAX] = {'\0'};
    int i, j, top;
    for(i = 0, j = 0, top = 0; infix[i] != '\0'; i++) switch(infix[i]) { 
        case '(':              // 運算子堆疊 
            stack[++top] = infix[i]; 
            break; 
        case '+': case '-': case '*': case '/': 
            while(priority(stack[top]) >= priority(infix[i])) { 
                postfix[j++] = stack[top--];
            } 
            stack[++top] = infix[i]; // 存入堆疊 
            break; 
        case ')': 
            while(stack[top] != '(') { // 遇 ) 輸出至 ( 
                postfix[j++] = stack[top--];
            } 
            top--;  // 不輸出 ( 
            break; 
        default:  // 運算元直接輸出 
            postfix[j++] = infix[i];
    }
    while(top > 0) { 
        postfix[j++] = stack[top--];
    }
} 

int priority(char op) { 
    switch(op) { 
        case '+': case '-': return 1;
        case '*': case '/': return 2;
        default:            return 0;
    } 
} 

double eval(char* infix) {
    char postfix[MAX]= {'\0'};
    char opnd[2] = {'\0'};
    double stack[MAX] = {0.0}; 
    
    inToPostfix(infix, postfix);    

    int top, i;
    for(top = 0, i = 0; postfix[i] != '\0'; i++) switch(postfix[i]) { 
        case '+': case '-': case '*': case '/': 
            stack[top - 1] = cal(postfix[i], stack[top - 1], stack[top]); 
            top--; 
            break; 
        default: 
            opnd[0] = postfix[i];
            stack[++top] = atof(opnd);
    }             
    
    return stack[top];
}

double cal(char op, double p1, double p2) { 
    switch(op) { 
        case '+': return p1 + p2; 
        case '-': return p1 - p2; 
        case '*': return p1 * p2; 
        case '/': return p1 / p2; 
    } 
}
