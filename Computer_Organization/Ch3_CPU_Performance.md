* CPU Exec Time 公式  (越**低**越好) 
   * Clock Cycle 公式
   * CPI 公式
   * 軟體如何影響效能 
   
* MIPS 公式 (越**高**越好) 
   * MIPS的三個問題
     * 沒有把每一指令的能力考慮進來
     * 同一台電腦**不同**程式，MIPS有可能**不同**
     * 可能跟效能成**反比**
   
* Amdahl's 定律 
   * Speedup公式
   * 改善**較常**出現部分的最佳化較有效
   
* 效能總評
   * 算術平均(Arithmetic Mean , AM) 
     * 每個程式執行次數相同
   * 加權算術平均(Weighted Arithmetic Mean , WAM) 
     * 執行次數不同，給加權值(頻率)
   * SPECratio 公式 (越**大**越好) 
     * 作正規化(用**算術平均數**作為效能總評可能會產生矛盾，改用幾何平均)
   * 幾何平均(Geometric Mean , GM) 
     * 先取幾何平均在做正規化 = 先做正規化再取幾何平均
     * 優 : 與**程式執行時間** & **用哪一台機器為正規化基準** 無關
     * 缺 : 違背效能測量基本原則 -> 無法測時間
 
 * 效能評估程式
    * 工作量(Workload) : always running program (簡單比較)
    * 效能評估程式(Benchmark) : (主要比較)
      * SPEC(System Performace Evaluation Coporation)   
      -> 最新 : SPEC CPU2000[12整數(CINT2000) & 14浮點數(CFP2000)]
        
        
