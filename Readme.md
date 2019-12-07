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

### AMD Athlon X2 1700Mhz ###

**test.TestArcTrig**
| Name | Accuracy | Speed |
| ---- | -------- | ----- |
| AtanSO1 | 0.0136638037737245 | 213.1517 |
| AtanMac | 0.0935295762042116 | 3797.3095 |
| AtanMac-16 | 0.0658025452458658 | 4476.5338 |
| AtanMac-32 | 0.0569347297414583 | 5217.8095 |
| AtanActon | 0.00226640192840606 | 1089.9242 |
| AtanActon-16 | 3.45821482145015E-08 | 1900.1575 |
| AtanActon-32 | 5.09453590424869E-14 | 3508.7304 |
| AtanAms | 5.64583344242475E-08 | 408.148 |
| Atanfdlibm | 122.793568849762 | 443.1341 |

**test.TestTrig**
| Name | Accuracy | Speed |
| ---- | -------- | ----- |
| SinSO | 1.10087619858681E-13 | 4249.7292 |
| Sin3 | 78.6051247042265 | 439.1145 |
| SinXupremZero | 0.0635031546303115 | 185.9578 |
| SinAms | 3.06749505990386E-08 | 299.0557 |
| Sin5 | 4.73341042766662E-05 | 364.5602 |
| Sin5-16 | 1.28681912630859E-13 | 548.9695 |
| Sin5-32 | 1.93002683562518E-13 | 934.2172 |
| Cordic-Sin | 0.301510668694616 | 3747.2538 |
| Cordic-Sin-24 | 4.7274776291645E-06 | 12637.42 |
| SinTaylor | 0.00400130951601307 | 212.638 |
| SinFdlibm | 2.261290882078 | 228.4435 |
| Sin6 | 14.075154510891 | 381.0937 |
| Sin6-16 | 6.75028061584193 | 595.37 |
| Sin6-32 | 3.30773444231687 | 1046.5789 |
| Cos | 4.28993996385435E-08 | 274.9541 |
| Cordic-Cos | 0.30451787266993 | 3702.4638 |
| Cordic-Cos-24 | 4.680740490739E-06 | 13189.1135 |

*Total Time: 65251*
