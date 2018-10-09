# Enhancing Performance with Pipelining
* [重點一：Pipeline](#重點一)
* [重點二：Pipeline datapath](#重點二)
* [重點三：Pipeline Control Unit](#重點三)
* [重點四：Pipeline hazard](#重點四)
* [重點五：Hazard Solution](#重點五)
* [重點六：Data hazard](#重點六)
* [重點七：Data dependency](#重點七)
* [重點八：Control hazard (Branch hazard)](#重點八)
* [重點九：Advanced pipeline](#重點九)
* [重點十：Pipeline Exception Handling](#重點十)
## 重點一
### Pipeline
* **管線化(Pipelining)** 
   * 多個指令重疊執行(Overlap execution)，同一時間平行處理不同Stage's job → 硬體使用率 ↑，throughput ↑
   * 將job的執行分成幾個Step，並將執行工作的硬體切割成幾個對應的Stage ( 1 stage execute 1 step )
   * 特性
      * **不會**對個別指令的延遲時間(latency : job真正時間，not延遲時間)有所幫助，但整體指令執行的throughput ↑
      * 多個指令同時使用不同資源 → 硬體使用率 ↑
      * Potential Speedup = number of pipeline stages
      * pipeline rate 受限於**最慢**pipeline stage的執行時間
      * pipeline stage 分割不平衡 → pipeline Speed ↓
      * fill & drain pipeline → pipeline Speed ↓
* **MIPS**
   * 所有指令`相同長度`( 32bits )
   * 少數指令的指令格式中`來源暫存器欄位`都在`相同位置`
   * 只有Load & Store 會存取記憶體
   * `運算元(operand)`必須在記憶體中`對齊(aligned)`
* **理想管線的執行及加速**(Far ideal pipeline)
   * Execution Time = [(S-1)+ N ] / T
   * CPI = [(S-1) + N ] / N ,if N → ∞ , CPI = 1
   * Speedup = S × N × T /{ [(S-1) + N ] × T' } , if N → ∞ ,Speedup = S
   * 1個指令花S-1個Clock通過管線，N個指令花(S-1) + N  
## 重點二
### Pipeline datapath
![image](https://user-images.githubusercontent.com/38349902/46470323-c712ed00-c808-11e8-82e7-b41e1719c42a.png)
## 重點三
### Pipeline Control Unit

![image](https://user-images.githubusercontent.com/38349902/46472178-ef054f00-c80e-11e8-97d1-a4d41ee9eb8d.png)  
## 重點四
### Pipeline hazard

* Structural hazards : 硬體資源不足，同時間內要執行多個指令卻無法執行 (e.g.IF,ME同時使用Single-Memory)
* Data harzards : 後面的指令需用到前面指令的結果(Data dependency)，但前面指令還在管線中因此無法獲得(指令距離≤2 in 5 stages MIPS pipeline)
* Control hazards : branch還沒決定要不要跳(之前教的是在MEM決定)，後面的指令已經進入pipeline了(進入IF,ID,EX中)，  
                    如果要跳那執行順序就會錯誤 → 又稱Branch hazards  
## 重點五                    
### Hazard Solution

* 3種Hazard皆可藉由暫停管線(Stall)來解決，But Clock Cycle Time ↑ , Performance ↓
* **Structural hazards**
  * Add Hardware
  * Stall (錯開指令並讓先進入pipeline的指令有較高優先順序使用硬體資源)
  * NOP 無法解決
* **Data harzards**
  * Software (Compiler)  
    * Insert NOP  
    * Instruction Reordering
  * Hardware  
    * Forwarding  
    * Detection → Stall(碰到load-use才會) → Forwarding  
    [補]: Data dependency
* **Control hazards**  
  * Software (Compiler)  
    * Insert NOP  
    * Delay Branch  
  * Hardware  
    * Predict not taken  
    * Flush wrong instruction
## 重點六    
### Data hazard
------
* **Software**    
    * Insert NOP    
      * NOP(No Operation) : 不幹嘛 ⇒ 不影響程式正確性  
      * 優點 : 簡單  
      * 缺點 : 效率差 (NOP佔時脈週期 e.g.2個NOP=佔2個Clock Cycle)  
    * Instruction Reordering/Pipeline scheduling    
      * 不影響程式執行的正確性下，指令順序重新排列，將會造成Data hazard的指令對(Instruction pair)間距離拉開
        (e.g. 拉開距離>2 in 5 stages MIPS pipeline)  
      * 優點 : 不會增加時脈週期  
      * 缺點 : 未必所有程式都可重排 → 適度加入NOP , 時脈週期增加)
* **Hardware**    
    * Forwarding(Bypassing) ： 加入特殊硬體來提早從內部資源獲取所缺少的項目 
    * Detection → Stall → Forwarding     
     ( i ) Detection  
         Step1. 偵測目前指令是否有指令寫入暫存器(Reg.Write : R-type/lw)  
         Step2. 目的暫存器不為0  
         Step3. 偵測目前指令之`目的暫存器`與其後指令之`來源暫存器rs,rt`是否相同  
         ![image](https://user-images.githubusercontent.com/38349902/46531153-c04ead80-c8ce-11e8-88ad-95f3e2f667e0.png)   
     ( ii ) Forwarding  
         ![image](https://user-images.githubusercontent.com/38349902/46534011-48d24b80-c8d9-11e8-9b15-47795f52e7d1.png)
         ![image](https://user-images.githubusercontent.com/38349902/46534044-6b646480-c8d9-11e8-9182-3e0d5c8916ed.png)  
         由於MEM/WB階段的結果是最先出來的，因此須將`MEM hazard`的偵測碼修改
         ![image](https://user-images.githubusercontent.com/38349902/46534664-80da8e00-c8db-11e8-8273-067612b955f7.png)  
     ( iii ) Stall    
         load-use data hazard : 當lw指令後跟著的指令需要讀取的來源暫存器 = lw 之目的暫存器  ⇒ hazard detection unit 解決  
         偵測碼  
         ![image](https://user-images.githubusercontent.com/38349902/46537189-4117a480-c8e3-11e8-9737-e1fba7623333.png)  
         ![image](https://user-images.githubusercontent.com/38349902/46540921-45e15600-c8ed-11e8-8adc-09738a965be5.png)  
## 重點七         
### Data dependency
* **RAW** ( read  after write ) 寫後讀 ( 唯一會在MIPS 2000造成hazard : True data dependecy )  
    ```
    add s0 , s1 , s2  
    sub t1 , s0 , t2
    ```
* **WAR** ( write after read  ) 讀後寫 ( False data dependecy )  
    ```
    add s0 , s1 , s2  
    sub s1 , t1 , t2
    ```
* **WAW** ( write after write ) 寫後寫 ( False data dependency )   
    ```
    add s0 , s1 , s2  
    sub s0 , t1 , t2  
    ```
## 重點八
### Control hazard (Branch hazard)
* **Software** (Compiler)  
    * Insert NOP          
    * Delay Branch (safty branch ) → hard
      * Compiler & Assembler 將不論branch有沒有跳都不會影響到的指令(照常執行)放到`branch delay slot`中
      * branch delay slot : branch delay都會執行到後面的指令位置，放`safty instruction`
      * 三種放法
      ![image](https://user-images.githubusercontent.com/38349902/46571511-c1540d80-c9a8-11e8-8577-f4afdbb5c8be.png)  
      * 優點 : 簡單、有效率
      * 缺點 : 當處理器管線延長、每個Clock分發指令數提高， 1 個 branch delay slot 已經不夠用了，雖然彈性高，但代價卻很大

         
* **Hardware**  
    * Predict not taken
    * Flush wrong instruction  
    ![image](https://user-images.githubusercontent.com/38349902/46568870-cbf9ad00-c97e-11e8-9906-d74e586ec439.png)
    ![image](https://user-images.githubusercontent.com/38349902/46568873-d6b44200-c97e-11e8-92ee-f8d2ac67020b.png)
* **Branch's data hazard**
    * Forwarding 
    ```
    add $1 , $2 , $3    //第1行
    add $4 , $5 , $6    //第2行
    ........            //第3行，這行隨便
    beq $1 , $4 ,target //第4行，beq的來源暫存器與1,2行的目的暫存器有data dependency
    ```
    * Stall 1 Clock
     ```
    lw  $1 , addr       //第1行
    add $4 , $5 , $6    //第2行
    Stall               //第3行
    beq $1 , $4 ,target //第4行，beq的來源暫存器與1,2行的目的暫存器有data dependency
    ```
    * Stall 2 Clock 
    ```
    lw  $1 , addr       //第1行
    Stall               //第2行
    Stall               //第3行
    beq $1 , $4 ,target //第4行，beq的來源暫存器與1,2行的目的暫存器有data dependency
    ```
    * 結論 : 與beq有data dependency，ALU指令需與beq間隔1行，lw則是2行
* **Branch Prediction**
    * Static  : 永遠猜跳或不跳 ( 上面介紹的是假設不跳 )
    * Dynamic : 用`Run Time Information`(以前幾次被執行的時候有沒有跳的紀錄)為依據來猜這次branch是否要，而Run 
    　　　　     Time Information存於`branch prediction buffur` 或 `branch history table (BHT)`  
　　　　         [註]BHT:由branch指令位址較低部分作為索引的小型記憶體，記憶體包含1-bit指示最近是否有branch發生
　　　　         [註]branch predictor算出branch是否成立，但計算目的位址在5 Stage popeline中，需要1 Clock(Penalty)   
　　　　         但可以透過以`branch target buffer`為cache，來存放目的地PC或是目的地指令來消除Penalty   
      * 1-bit prediction scheme : 錯一次改猜別的 (見風轉舵型)
      * 2-bit prediction scheme : 強猜跳 ↔ 弱猜跳 ↔ 弱猜不跳 ↔ 強猜不跳  
      * correlating predictor：2-bit只使用特定branch資訊，但同時使用local & global branch執行資訊  
      　　　　　　　　　       ，在相同個prediction bit下，準確度更高
      * tournament predictor：每個branch使用多個predictor，追蹤看哪個predictor產生比較好的結果，再用選擇器
      　　　　　　　　　　      決定要用誰做預測
## 重點九
### Advanced pipeline 
* **Instruction-level Parallelism (ILP)** : 管線充分利用指令之間潛在的平行度
* **提升ILP的方法**
    * Increase pipeline depth(長度不變)，切更多的Stage → Superpipeline (MIPS不是)
      * 優點：Speedup ↑ (∵ Clock縮短了)
      * 缺點：a. hazard ↑   
      　　　b. 較難平衡各Stage的時間
      * 解決：a. 平衡Stage  
      　　　b. 用pipeline scheduling               
    * Mutiple issue : 大量複製pipeline功能單元讓1個Clock可塞多個指令 ⇒ 使CPI < 1
      * 實作：a. static multiple： program compile in compiler 時 (執行前)  
      　　　b. dynamic mulitiple： program execute in processor 時 (執行中) → 又稱 Superscaler　　　
      * 解決：a. Pack instruction : packing instr.in issue slot → stactic : compiler
      　　　　　　　　　　　　　　　　　　　　　　　→ dynamic : processor   
      　　　b. handle data/control hazard：→ stactic : compiler  
         　　　　　　　　　　　　　　　　　→ dynamic : Hardware
      * Static multiple issue MIPS ISA Example
        * 一次分發兩個指令 : 整數型ALU/Branch(前32bits) + lw/sw(後32bits) = 64 bits
      * Dynamic multiple issue proceesor (Superscaler)
* **Speculation**
    * 猜測允許Compiler或是Processor去猜測某指令的性質，以便能夠執行其他和這個被猜測指令相關的指令
    * Software
      * Insert check code
      * provide fix software
    * Hardware
      * buffer result
      * flush buffer if guess wrong
* **Code Scheduling**
* **超長指令集VLIW**
    * 早期 : 每個指令做的事很少，造價貴，基本指令格式超長，hardware有多少，欄位就如何      
            現今 : hardware變便宜，實現VLIW，Static mutilple issue ~ VLIW
    * 優點 
      * Simpler hardware
      * More Scalable (容易規模擴增)
    * 缺點
      * Programmer/Compiler complexity and longer comoilation times
      * hazard 無法解決就只能Stall
      * 不一樣 Object(binary)code incompatibility(不相容)
      * Program memory bandwidth 需求高
      * loop unrolling 造成 code bloat(爆炸)
 * **Intel IA-64 架構**
     * IA-64 ~ MIPS-64，為利用Reg tp Reg做運算的RISC指令集
     * Explicitly Parallel Instruction Computer(EPIC):利用Compiler開發的平行度
     * IA-64 & MIPS 架構差異  
     
     |            | IA-64                                                                        | MIPS |
     |:----------:|:----------------------------------------------------------------------------:|:----:|
     |Register數量|128個整數 + 128個浮點數 + 8個branch + 64個1-bit condition                       |      |
     |            |將有固定格式並標明相依性的指令裝在一起                                           |      |
     |            |包含特別指令，有能力做到猜測並且將(部分)branch消除 ⇒ 消除control hazard ⇒ ILP ↑  |      |
     * IA-64 有VLIW的優點，且比VLIW更有彈性
     * 兩種獲得彈性的概念
       * Instruction Group : 一連串沒有Reg Data dependency的指令，足夠硬體 ⇒ 平行執行
       * Bundle : template field(5-bit) + 3個指令(3*41-bit=123bit) = 128 bits    
         [註] template field : 5-bit 個別標出bundle要用到的5個執行單元的哪一個  
         (整數ALU,非整數型ALU,記憶體單元,浮點數單元,Branch處理單元)
       * Prediction : 利用Condition指令來取代原有的Branch指令來消除Branch ⇒ 消除control hazard ⇒ ILP ↑  

* **Out-of-order Execution**
## 重點十
### Pipeline Exception Handling
* **Exception & Interrupt**
* **Handling Exception**
* **OS Exception**
* **Handling Exception Steps**
* **Imprecise interrupt**
       
     
 
    
         
    

    
  

   





                
                

                


              


  
                
                


             
             
             
             
          
            
            
            

    

