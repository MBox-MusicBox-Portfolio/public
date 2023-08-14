import dotenv from 'dotenv';
import {RedisGetValue} from '../../modules/redis.js';
import User from '../../models/user.js';
dotenv.config();

export async function getRedisRecord()
{

}

export async function getUser()
{
    // const user = User.findOne({where:{Email: object.Email}, attributes:{Email:}); 
}

export async function changePassword(context,ctx)
{
    if(!context){

    }
}