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
    * Structural hazards : 硬體資源不足，同時間內要執行多個指令卻無法執行 (e.g.同時使用Mem)
    * Data harzards : 後面的指令需用到前面指令的結果(Data dependency)，但前面指令還在管線中因此無法獲得
    * Control hazards : branch還沒決定要不要跳，後面的指令已經進入pipeline了(指令距離≤2)，  
      如果要跳那執行順序就會錯誤 → 又稱Branch hazards
* **
    

