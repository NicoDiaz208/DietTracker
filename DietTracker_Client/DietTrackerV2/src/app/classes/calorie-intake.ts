import { ICalorieIntake } from '../interfaces/icalorie-intake';

export class CalorieIntake implements ICalorieIntake {
  id: number;
  goal: number;
  current: number;
}
