import { UserService } from 'src/app/services/api/user.service';
import { User } from 'src/app/services/model/user';

export class UsernameValidator {
  userVal: User;

  constructor(private userService: UserService ){

  }
  async validUsername(username: string){

    this.userVal = await this.userService.apiUserGetSingleUserByUsernameGet(username).toPromise();
    if(this.userVal.name == null){
      return ({validUsername: true});
    } else {
      return (null);
    }
  }
}
