#include <intrinsics.h>
#include <assert.h>
#include "arm_comm.h"

void NVIC_IntEnable(Int32U IntNumber);
void NVIC_IntDisable(Int32U IntNumber);
void NVIC_ClrPend(Int32U IntNumber);
void NVIC_IntPri(Int32U IntNumber, Int8U Priority);