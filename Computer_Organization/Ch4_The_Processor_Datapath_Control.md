* 抽象化設計 
   * Specification
     * Instruction set
     1. Memory Access : lw,sw
     2. Arithmetic Logic : add,sub,and,or,slt
     3. Flow Contrl : beq,j
   * Machines (3個)
     * Single Cycle Machine(CPI=1)[Datapath + Control Unit]   
     1. 指令執行需在`single-cycle`內完成  
     [∴Clock Cycle Time = 執行時間最長指令的Time -> Clock Cycle Time變很長(inefficient)]
     2. 改善方法：(i)使用較短的時脈週期，一個指令用數個時脈完成 (ii)Pipeline
     * Mulitiple Cycle Machine(CPI = 指令Step數)[Datapath + Control Unit(Hardware + Microprogramming)]
     1. 每個Steps在執行中會花掉1 clock cycle,使得不同的instructions花費不同數量之clock cycle
     2. 執行時間較`長`instruction → steps較`多` → clock cycle `多`  
        執行時間較`短`instruction → steps較`少` → clock cycle `少` 
     3. 限制每一Step只能使用一個主要功能單元(7個Components)
     4. 功能單元共享 → 硬體需求下降
     * Pipeline[Datapath + Control Unit + Hazards + Advance pipeline]
   * Components (7個)
     * Instruction Memory 
     * Register File
     * Data Memory
     * ALU
     * Adder
     * Program Counter(PC)
     * Sign Extension Unit(SE)
 
 * Single Cycle Machine Datapath 建構
![image](https://user-images.githubusercontent.com/38349902/46389561-25ef3e00-c705-11e8-8c88-60bd876714e2.png)
 
 * Single Cycle Machine Control Unit 建構
    * Mulitilevel Control
      * 優點  
      1. `降低`Control Unit `大小`
      2. `加快`Control Unit `速度`
    * ALU Control Design
      * ALU Control Table
      * ALU Control Truth Table -> Boolean Function -> Circuit 
      * Main Control Truth Table -> Circuit (PLA)  
      
* Single Cyele Machince 效能
    * 不同指令之關鍵路徑(Critical Path) 
    
| Instruction | Instruction Memory | Reg.Read | ALU OP | Data Memory | Reg.Write |
| :---------: | :----------------: | :------: | :----: | :---------: | :-------: |
| R-type      |√                   |√         |√       |             |√          |
| Load word   |√                   |√         |√       |√            |√          |        
| Store word  |√                   |√         |√       |√            |           |      
| Branch      |√                   |√         |√       |             |           | 
| Jump        |√                   |          |        |             |           |

* Mulitiple Cycle Machine 製作 Datapath

| Instruction | Instruction Memory | Reg.Read | ALU OP            | Data Memory | Reg.Write | CPI |
| :---------: | :----------------: | :------: | :----:            | :---------: | :-------: |:---:|
| R-type      |IF                  |ID/RF     |Execution          |             |WB         |4    |
| Load word   |IF                  |ID/RF     |Addr.calculation   |MA           |WB         |5    |    
| Store word  |IF                  |ID/RF     |Addr.calculation   |MA           |           |4    |  
| Branch      |IF                  |ID/RF     |Compare Reg.       |             |           |3    |
| Jump        |IF                  |ID        |Compose target addr|             |           |3    |


     
