# AltMAth #
Alternative Math functions

I discovered that some math functions give different answers depending on which computer I was using. When I went looking for why, I found that certain math functions are directly implemented in the (x86) CPU.

Wikipedia shows some of these instructions.
* https://en.wikipedia.org/wiki/X86_instruction_listings#Original_8087_instructions
* https://en.wikipedia.org/wiki/X86_instruction_listings#Added_with_80387

The ones that caught my eye were the trig functions:
* FPATAN
* FPTAN
* FCOS
* FSIN
* FSINCOS
* FSQRT

## TODO ##
* Review functions that are way off (innacurate)
* Do Cross platform testing
* Add more functions

## Speed Test Results ##
*all times in milliseconds*

| Processor            | Label | Total Time |
| -------------------- | ----- | ---------- |
| AMD Athlon X2 1.7GHz | A     | 65251      |
| Intel i3 530 2.93GHz | I     | 31358      |

**test.TestArcTrig**

| Name         | Accuracy - A         | Speed - A | Accuracy - I         | Speed - I |
| ------------ | -------------------- | --------- |--------------------- | --------- |
| AtanSO1      | 0.0136638037737245   | 213.1517  | 0.0136638037737241   | 68.943    |
| AtanMac      | 0.0935295762042116   | 3797.3095 | 0.0935295762042112   | 1260.5377 |
| AtanMac-16   | 0.0658025452458658   | 4476.5338 | 0.0658025452458654   | 1455.6221 |
| AtanMac-32   | 0.0569347297414583   | 5217.8095 | 0.0569347297414578   | 1614.5611 |
| AtanActon    | 0.00226640192840606  | 1089.9242 | 0.00226640192840601  | 530.4486  |
| AtanActon-16 | 3.45821482145015E-08 | 1900.1575 | 3.45821481589903E-08 | 913.3468  |
| AtanActon-32 | 5.09453590424869E-14 | 3508.7304 | 5.04457586814056E-14 | 1711.377  |
| AtanAms      | 5.64583344242475E-08 | 408.148   | 5.64583343687364E-08 | 201.6743  |
| Atanfdlibm   | 122.793568849762     | 443.1341  | 122.793568849762     | 220.6983  |

**test.TestTrig**

| Name          | Accuracy - A         | Speed - A  | Accuracy - I         | Speed - I |
| ------------- | -------------------- | ---------- | -------------------- | --------- |
| SinSO         | 1.10087619858681E-13 | 4249.7292  | 1.09810064102525E-13 | 8500.9257 |
| Sin3          | 78.6051247042265     | 439.1145   | 78.6051247042265     | 231.0808  |
| SinXupremZero | 0.0635031546303115   | 185.9578   | 0.0635031546303115   | 81.8284   |
| SinAms        | 3.06749505990386E-08 | 299.0557   | 3.06749508765944E-08 | 127.8584  |
| Sin5          | 4.73341042766662E-05 | 364.5602   | 4.73341042769437E-05 | 175.7506  |
| Sin5-16       | 1.28681912630859E-13 | 548.9695   | 1.28626401479628E-13 | 259.3699  |
| Sin5-32       | 1.93002683562518E-13 | 934.2172   | 1.93058194713749E-13 | 425.5533  |
| Cordic-Sin    | 0.301510668694616    | 3747.2538  | 0.301510668694616    | 1464.9103 |
| Cordic-Sin-24 | 4.7274776291645E-06  | 12637.42   | 4.72747762933103E-06 | 4361.2589 |
| SinTaylor     | 0.00400130951601307  | 212.638    | 0.00400130951601312  | 100.7131  |
| SinFdlibm     | 2.261290882078       | 228.4435   | 2.261290882078       | 106.4183  |
| Sin6          | 14.075154510891      | 381.0937   | 14.075154510891      | 132.8478  |
| Sin6-16       | 6.75028061584193     | 595.37     | 6.75028061584193     | 210.7845  |
| Sin6-32       | 3.30773444231687     | 1046.5789  | 3.30773444231687     | 360.3422  |
| Cos           | 4.28993996385435E-08 | 274.9541   | 4.28993995275212E-08 | 111.4881  |
| Cordic-Cos    | 0.30451787266993     | 3702.4638  | 0.30451787266993     | 1421.8445 |
| Cordic-Cos-24 | 4.680740490739E-06   | 13189.1135 | 4.68074049085002E-06 | 4274.8336 |
