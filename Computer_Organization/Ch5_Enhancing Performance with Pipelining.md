# Enhancing Performance with Pipelining
* [(一)Pipeline](#(一)Pipeline)
* [(二)Pipeline datapath](#(二)Pipeline datapath)
* [(三)Pipeline Control Unit](#(三)Pipeline Control Unit)
* [(四)Pipeline hazard](#(四)Pipeline hazard)
* [(五)Hazard Solution](#(五)Hazard Solution)
* [(六)Data hazard](#(六)Data hazard)
* [(七)Data dependency](#(七)Data dependency)

### (一)Pipeline
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
## (二)Pipeline datapath
![image](https://user-images.githubusercontent.com/38349902/46470323-c712ed00-c808-11e8-82e7-b41e1719c42a.png)
## (三)Pipeline Control Unit
![image](https://user-images.githubusercontent.com/38349902/46472178-ef054f00-c80e-11e8-97d1-a4d41ee9eb8d.png)
## (四)Pipeline hazard
* Structural hazards : 硬體資源不足，同時間內要執行多個指令卻無法執行 (e.g.IF,ME同時使用Single-Memory)
* Data harzards : 後面的指令需用到前面指令的結果(Data dependency)，但前面指令還在管線中因此無法獲得(指令距離≤2 in 5 stages MIPS pipeline)
* Control hazards : branch還沒決定要不要跳(之前教的是在MEM決定)，後面的指令已經進入pipeline了(進入IF,ID,EX中)，  
                    如果要跳那執行順序就會錯誤 → 又稱Branch hazards
## (五)Hazard Solution
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
## (六)Data hazard
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
## (七)Data dependency
* RAW ( read  after write ) 寫後讀 ( 唯一會在MIPS 2000造成hazard : True data dependecy )  
    ```
    add s0 , s1 , s2  
    sub t1 , s0 , t2
    ```
* WAR ( write after read  ) 讀後寫 ( False data dependecy )  
    ```
    add s0 , s1 , s2  
    sub s1 , t1 , t2
    ```
* WAW ( write after write ) 寫後寫 ( False data dependency )   
    ```
    add s0 , s1 , s2  
    sub s0 , t1 , t2  
    ```
## (八) Control hazard (Branch hazard)
* Software (Compiler)  
  * Insert NOP  
            
  * Delay Branch    
         
* Hardware  
  * Predict not taken  
  * Flush wrong instruction



                
                

                


              


  
                
                


             
             
             
             
          
            
            
            

    

