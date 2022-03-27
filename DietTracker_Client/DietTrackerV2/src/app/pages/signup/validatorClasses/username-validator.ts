import { UserService } from 'src/app/services/api/user.service';
import { User } from 'src/app/services/model/user';

export class UsernameValidator {
  userVal: string;

  constructor(private userService: UserService ){

  }
  async validUsername(username: string){

    this.userVal = await this.userService.apiUserGetSingleUsernameGet(username).toPromise();
    if(this.userVal == null){
      return ({validUsername: true});
    } else {
      return (null);
    }
  }
}
