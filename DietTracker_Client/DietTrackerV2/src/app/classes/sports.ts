import { ISports } from '../interfaces/isports';

export class Sports implements ISports{
  activeTime: string;
  burnedCalories: number;
  distance: number;
  id: number;
  steps: number;
}
