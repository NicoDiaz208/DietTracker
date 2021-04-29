import { IDailyProgress } from '../interfaces/idaily-progress';

export class DailyProgress implements IDailyProgress{
  id: number;
  name: string;
  progress: string;
}
