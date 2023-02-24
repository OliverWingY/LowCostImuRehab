function [successfullInitialisation] = InitialiseMatlabWorkspace()
successfullInitialisation = false;
% put whatever needs to be in the workspace to start with in here
assignin('base', "zero", 0);

successfullInitialisation = true;