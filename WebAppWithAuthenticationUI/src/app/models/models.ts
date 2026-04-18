import {UserStatus} from '../enums/models';

interface BaseDto {
  id: number;
}

export interface UserDto extends BaseDto {
  name: string;
  surname: string;
  email: string;
  position: string;
  lastLoginDate?: string;
  status: UserStatus;
}
