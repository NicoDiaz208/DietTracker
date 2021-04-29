import { IWaterIntake } from '../interfaces/iwater-intake';

export class WaterIntake implements IWaterIntake{
  id: number;
  glassesOfWaterGoal: number;
  glassesOfWaterCurrently: number;
}
