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

export interface LoginRequestDto {
  email: string;
  password: string;
  rememberMe: boolean;
}

export interface SignupRequestDto {
  name: string;
  surname: string;
  position?: string;
  email: string;
  password: string;
  rememberMe: boolean;
}
