import { UserContractDto } from './userContractModel';

export interface UserDto {
  userId: number;
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  userContracts: UserContractDto[];
}
