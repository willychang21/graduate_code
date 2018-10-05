### (一) Pipeline
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
* **Pipeline datapath**
![image](https://user-images.githubusercontent.com/38349902/46470323-c712ed00-c808-11e8-82e7-b41e1719c42a.png)
* **Pipeline Control Unit** 
![image](https://user-images.githubusercontent.com/38349902/46472178-ef054f00-c80e-11e8-97d1-a4d41ee9eb8d.png)
* **Pipeline Hazard**
    * Structural hazards : 硬體資源不足，同時間內要執行多個指令卻無法執行 (e.g.IF,ME同時使用Memory)
    * Data harzards : 後面的指令需用到前面指令的結果(Data dependency)，但前面指令還在管線中因此無法獲得
    * Control hazards : branch還沒決定要不要跳，後面的指令已經進入pipeline了(指令距離≤2 in 5 stages MIPS pipeline)，    
      如果要跳那執行順序就會錯誤 → 又稱Branch hazards
* **Hazard Solution**
    * 3種Hazard皆可藉由暫停管線(Stall)來解決，But Clock Cycle Time ↑ , Performance ↓
    * Structural hazards
      * Add Hardware
      * Stall (錯開指令並讓先進入pipeline的指令有較高優先順序使用硬體資源)
      * NOP 無法解決
    * Data harzards
      * Software (Compiler)  
      a. Insert NOP  
      b. Instruction Reordering
      * Hardware  
      a. Forwarding  
      b. Detection → Stall(碰到load-use才會) → Forwarding  
      [補]: Data dependency
    * Control hazards  
      * Software (Compiler)  
      a. Insert NOP  
      b. Delay Branch  
      * Hardware  
      a. Predict not taken  
      b. Flush wrong instruction
   * **Data hazard**
       * Software  
          a. Insert NOP  
            NOP(No Operation) : 不幹嘛 ⇒ 不影響程式正確性  
            優點 : 簡單  
            缺點 : 效率差 (NOP佔時脈週期 e.g.2個NOP=佔2個Clock Cycle)  
          b. Instruction Reordering/Pipeline scheduling  
            不影響程式執行的正確性下，指令順序重新排列，將會造成Data hazard的指令對(Instruction pair)間距離拉開
            (e.g. 拉開距離>2 in 5 stages MIPS pipeline)  
            優點 : 不會增加時脈週期  
            缺點 : 未必所有程式都可重排 → 適度加入NOP , 時脈週期增加)
       * Hardware  
          a. Forwarding(Bypassing)
             加入特殊硬體來提早從內部資源獲取所缺少的項目  
          b. Detection → Stall → Forwarding   
             　　(i) Detection  
             　　　Step1. 偵測目前指令是否有指令寫入暫存器(Reg.Write : R-type/lw)  
             　　　Step2. 目的暫存器不為0  
             　　　Step3. 偵測目前指令之`目的暫存器`與其後指令之`來源暫存器rs,rt`是否相同  
                ![image](https://user-images.githubusercontent.com/38349902/46531153-c04ead80-c8ce-11e8-88ad-95f3e2f667e0.png) 
                (ii) Forwarding  
                ![image](https://user-images.githubusercontent.com/38349902/46534011-48d24b80-c8d9-11e8-9b15-47795f52e7d1.png)
                ![image](https://user-images.githubusercontent.com/38349902/46534044-6b646480-c8d9-11e8-9182-3e0d5c8916ed.png)  
                由於MEM/WB階段的結果是最先出來的，因此須將`MEM hazard`的偵測碼修改
                ![image](https://user-images.githubusercontent.com/38349902/46534664-80da8e00-c8db-11e8-8273-067612b955f7.png)  
                (iii) Stall    
                load-use data hazard : 當lw指令後跟著的指令需要讀取的來源暫存器 = lw 之目的暫存器    
                偵測碼  
                ![image](https://user-images.githubusercontent.com/38349902/46537189-4117a480-c8e3-11e8-9737-e1fba7623333.png)  
                ![image](https://user-images.githubusercontent.com/38349902/46540074-e2eebf80-c8ea-11e8-8a84-a3b3b55380bf.png)


                
                

                


              


  
                
                


             
             
             
             
          
            
            
            

    

