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