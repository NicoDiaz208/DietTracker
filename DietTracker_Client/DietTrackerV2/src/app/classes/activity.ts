import{IActivity} from '../interfaces/iactivity';
export class Activity implements IActivity {
  id: number;
  steps: number;
  activeTime: number;
  burnedCalories: number;
}
