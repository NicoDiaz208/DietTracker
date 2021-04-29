import { ISleep } from '../interfaces/isleep';
export class Sleep implements ISleep{
  id: number;
  goalTimeSleep: number;
  houersOfSleep: number;
}
