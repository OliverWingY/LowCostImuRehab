function [Angles] = ImuAngleToGlobalAngle(imuAngles)
HumerousMatrix = [2,0,0;0,2,0;0,0,2]; %Todo: measure and calculate the correct matrices
RadiusMatrix = [0,0,1;0,1,0;1,0,0];
Angles = zeros(6);
Angles(1:3) = imuAngles(1:3)*HumerousMatrix;
Angles(4:6) = imuAngles(4:6)*RadiusMatrix;