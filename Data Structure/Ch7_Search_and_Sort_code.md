### BinarySearch_Iteration
```c++
void BinarySearch_Iteration(A[],int key,int n)
{
	l = 1;  //l:left  最左邊
	r = n;  //r:right 最右邊
	while(l<=r)
	{
		m = (l + r)/2;
		switch(Compare(key,A[m]))
		{
			case "==" : return m;  //找到了
			case "<"  : r = m - 1;  //最右邊到m-1
			case ">"  : l = m + 1;  //最左邊到m+1
		}
	}
	return 0; //找不到key
}
```
### BinarySearch_Recursive
```c++
void BinarySearch_Recursive(A[],int l,int r,int key)
{
	if(l<=r)
	{
		m = (l + r)/2;
		switch(Compare(key,A[m]))
		{	
		    case "==" : return m;   //找到了
			case "<"  : BinarySearch_Recursive(A[],l,m-1,key);
			case ">"  : BinarySearch_Recursive(A[],m+1,r,key);  
		}
	}
	return 0; //找不到key
}
```
### Insert
```c++
void Insert(A[],r,i)
{
	//將r插進A[0]~A[i]已經排好的陣列
	j = i;
	while(r < A[i]) //從最大開始往前比
	{
		A[j+1] = A[j]; //比r大的往後搬
		j--;        //往前
	}
	A[j+1] = r;
}
void Insert_main(A[],n)
{
	A[0] = -∞;   //防止如果r是子串列的min,會overflow
	for(i=2;i<=n;i++)
	{
		Insert(A,A[2],i-1);  //從第2個開始排列
	}
}
```
### SelectionSort
```c++
void SelectionSort(A[],n)
{
	for(i=1;i<=n;i++)
	{
		min = i;
		for(j=i+1;j<=n;j++)
		{
			if(A[j]<A[min])  //比min小，取代min
			{
				min = j;
			}
		}
		if(min!=i)
		{
			swap(A[i],A[min]);
		}
	}
}
```
### BubbleSort
```c++
void BubbleSort(A[],n)
{
	for(i=1;i<=n-1;i++)  //最多做(n-1)回合
	{
		f = 0;
		for(j=1;j<=n-i;j++)
		{
			if(A[j]>A[j+1]);
			swap(A[j],A[j+1];
			f=1;
		}
		if(f==0)
		{
			exit;
		}
	}
}	
```
### ShellSort
```c++
void ShellSort(A[],n)
{
	span = n/2;
	while(span>=1)  //算到span = 1 為止
	{
		do
		{
			f = 0;  //每輪都將f reset,以利看有沒有swap
			for(i=1;i<=n-span;i++)
			{
				if(A[i]<A[i+span])
				{
				swap(A[i],A[i+span]);
				f = 1;
				}
			}
		}
		while(f!=0);//無swap跳出
		span = span/2;
	}
}
```
### QuickSort_DS
```c++
void QuickSort_DS(A[],l,r)
{
	if(l < r)
	{
		pk = A[l];  //以最左為初始pk
		i = l;
		j = r + 1;
        do
		{
			do
			{
				i = i + 1;   //往後
			}
			while(A[i]<pk);  //直到找到比pk大的
			do
			{
				j = j - ;    //往前
			}
			while(A[j]>pk);  //直到找到比pk小的
			if(i<j)
			{
				swap(A[i],A[j]);  //交換
			}	
		}
		while(i<j);
		swap(A[l],A[j]); //將pk放到正確位置
		QuickSort_DS(A[],l,j-1); //右邊QuickSort
		QuickSort_DS(A[],j+1,r); //左邊QuickSort
	}
}
```
### QuickSort_Algo
```c++
void QuickSort_Algo(A[],p,r)
{
	if(p<r)
	{
		q = Partition(A[],p,r);
		QuickSort_Algo(A[],p,q-1);
		QuickSort_Algo(A[],q+1,r);
	}
}
void Partition(A[],p,r)
{
	x = A[r]; //最右邊當pk
	i = P - 1;//p的前1格,方便後面算
	for(j=p;j<=r-1;j++) //從頭部掃到r-1
	{
		if(A[j]<=x)         //小於等於pk
		{
			i = i + 1;      //i先往前移
			swap(A[i],A[j]);//再交換
		}
	}
	swap(A[r],A[i+1]);      //pk跟最後一個換的"下一個"交換
	return (i+1);           //回傳最後一個換的"下一個"位置
}
```
### MergeSort_Recursive
```c++
void MergeSort_Recursive(A[],l,u,p[])
{   //A[l]~A[u]排序形成new runs p[]
    if(l>=u)  //只有一筆
	{
		return p[] = A[l];
	}
	else
	{
		m = (l+u)/2;
		MergeSort_Recursive(A[],l,m,q[]);   //A[l]~A[m]排序形成new run q[]
		MergeSort_Recursive(A[],m+1,u,r[]); //A[M+1]~A[U]排序形成new run r[]
		MergeTwoRuns(A[],q[],r[],P[]);      //q[],r[]合併
	}
}
```
### HeapSort
```c++
void HeapSort(Tree,n)
{
	for(i=n/2;i>=1;i--)
	{
		AdjustHeap(tree,i,n);  //將tree化為MAX-Heap
	}
    for(i=n-1;i>=1;i--)
	{
		swap(tree[1],tree[i+1]); //將root(MAX)與最後一個(n)node交換
		AdjustHeap(tree,1,i);    //剩下的化為MAX-Heap
	}
}
```
### Find_ith_min
```c++
void Find_ith_min(A[],p,r,i)
{
	//在A[P]~A[r]中找出i'th小
	q = Partition(A[],p,r); //找出pk的正確位置q
	k = q - p + 1;          //q是第 k'th 小
	if(i == k)
	{
		return A[q];
	}
	else if(i<k)
	{
		return Find_ith_min(A[],p,q-1,i);
	}
	else
	{
		return Find_ith_min(A[],q+1,r,i-k);  //i-k:因為分開算要把前面k個減掉
	}
}
```   		

