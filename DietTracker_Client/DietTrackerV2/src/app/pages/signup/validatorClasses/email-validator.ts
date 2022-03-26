import { UserService } from 'src/app/services/api/user.service';
import { User } from 'src/app/services/model/user';
export class EmailValidator {
  userVal: User;

  constructor(private userService: UserService ){

  }
  async validEmail(email: string){

    this.userVal = await this.userService.apiUserGetSingleBMRGet(email).toPromise();
    if(this.userVal.name == null){
      return ({validUsername: true});
    } else {
      return (null);
    }
  }
}
