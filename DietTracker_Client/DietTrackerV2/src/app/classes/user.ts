import { IUser } from '../interfaces/iuser';

export class User implements IUser{
  id: number;
  name: string;
  phoneNumber: string;
  email: string;
  dateOfBirth: string;
  gender: string;
  weight: number;
  height: number;
  activityLevelEstimate: string;
}
