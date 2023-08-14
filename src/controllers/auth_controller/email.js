import dotenv from 'dotenv';
import {RedisDelKey, RedisGetKey, RedisGetValue} from '../../modules/redis.js';
import User from '../../models/user.js';
dotenv.config();
const response={
    success:false,
    value:{}
}

export async function confirmEmail(object, ctx) {
    /***
     * TODO Сделать проверку на null 
*/
    try{
        let key = await RedisGetValue(object.activateKey);
        //let stringDbKey = key.replace(/^"(.*)"$/, '$1');
        const user = await User.findOne({where:{Id:key}, attributes:['Id', 'isEmailVerify']});
              user.IsEmailVerify = true;
        const result = await user.save();
            await delKey(key);
            ctx.status = 200;
              response.success=true;
              response.value={email:"Email is confirmed"}
              //const keys = 'KeyEmail_05756ac2-41a1-4a98-afd4-8d7774668250';
            ctx.body=response;
        //return response;
    }catch(ex){
        ctx.status = 500;
             response.success=false;
             response.value={error: `${ex}`}      
        ctx.body=response;  
    }
    
}

export async function checkKeyExpiration(key)
{
  if(key){ return (RedisGetKey ? true : false);}
}

export async function delKey(key)
{
    await RedisDelKey(key);
}