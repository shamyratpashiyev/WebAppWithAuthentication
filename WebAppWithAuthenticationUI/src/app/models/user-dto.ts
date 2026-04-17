import {UserStatus} from '../enums/user-status';

export interface UserDto {
  id: number;
  name: string;
  surname: string;
  email: string;
  position: string;
  lastLoginDate?: string;
  status: UserStatus;
}
