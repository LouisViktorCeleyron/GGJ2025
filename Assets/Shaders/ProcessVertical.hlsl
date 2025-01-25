//UNITY_SHADER_NO_UPGRADE
#ifndef PROCESS_VERTICAL_INCLUDED
#define PROCESS_VERTICAL_INCLUDED

void ProcessVertical_float(
	bool verticalDisplacement,
	float X_in,
	float Y_in,
	float processedNoise,
	out float X_out,
	out float Y_out
){
	if (verticalDisplacement) {
		X_out = X_in;
		Y_out = Y_in + processedNoise;
	} else {
		X_out = X_in + processedNoise;
		Y_out = Y_in;
	}
}
#endif

// vim: ft=hlsl
