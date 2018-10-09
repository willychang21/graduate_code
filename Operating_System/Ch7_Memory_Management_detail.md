# Memory Management
* [Binding](#Binding)

## Binding
* **Binding** : 決定程式執行之起始位址
* **Stactic Binding** ( logical address = physical address )
   * **Compiling Time** (Compiler) → 如果編譯時，程式所在的記憶體位置已知，那麼可產生absolute code，將來程式執行的起始位址不得變更 ； 如果起始位置變化，必須重新編譯代碼。
       * 缺點：若所決定的位址內有其它的程式在執行，或之後要變更程式執行的起始位址，則須 recompile。
        
   * **Loading Time** (linking loader) → 如果編譯時不能確定程式所在的記憶體位置，則必須生成relocatable code
     * 缺點 
       * execution time 沒有被呼叫到的模組仍需事先 linking, Allocation, Loading，浪費時間也浪費記憶體。 (e.g. if-else 的程序、OS 錯誤處理程序。)
       * 程式執行期間仍不可以改變起始位址。
       ![image](https://user-images.githubusercontent.com/38349902/46678039-5576dc80-cc16-11e8-9190-252398db7a76.png)  
* **Dynamic Binding** ( logical address ≠ physical address ) 
  * **Execution Time** (O.S) → 在程式執行期間才決定程式執行的起始位址。需要的額外硬體支援(Memory-Management Unit, MMU)。
    * 優點
      * [1]：Process在執行期間可以任意更動其起始位址，仍能正確執行
      * [2]：有利於O.S彈性地管理Memory Space之配置
    * 缺點
      * [1]：需要Hardware額外支持 → 不是O.S(∵要用O.S需interrupt，而此操作太頻繁不適合)
      * [2]：Process Exec.Time 變長 ，Performance 較差
    * Paging是用Dynamic binding，雖然Paging也可以用Compiling Time 或 linking Time，但沒什麼意義
* Logical address : CPU → Logical address (user site) → Logical address space      
  Physical address : Memory → Physical address (Hareware site e.g RAM) → Physical address space
* **Dynamic loading** 
    
    
