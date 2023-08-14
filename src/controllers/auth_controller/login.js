import dotenv from 'dotenv';
import { createJWT, decodeJWT } from '../../modules/token.js';
import * as validator from '../../modules/validator.js';
import { v4 } from 'uuid';
import * as redis from '../../modules/redis.js';
import User from '../../models/user.js';
import { isConfirmEmail } from './email.js';

dotenv.config();

const response={
    success:false,
    value:{}
}
const authKey = {
    userKeyEntity: "",
    token: ""
}

/**
 * Валидирование формы и данных в базе данных 
 * Form and Db data validation 
 * @param {*} object 
 * @param {*} context 
 */
export async function validationLoginForm(object, context) {
    try {
        const validation = await validator.getValidationAuthorization(object);
        if(validation === null && validation.error)
        {
            context.status=400;
            context.body= await validator.parserErrorString(validation);
        }else{
            const {Email,Password} = context.request.body;
                const user = await User.findOne({ where: {Email : object.Email}, attributes: ['Id','Name', 'Email', 'RoleId', 'IsEmailVerify', 'IsBlocked', 'Birthday', 'Password'] });
                const role = await user.getRole();
            const dbValidate = await validator.databaseValidator(context.request.body,user,context);
            context.body=dbValidate;
        if(dbValidate === true)
        {
            // Перестроить user перед отправкой
            let jwtObject = await fillJWTUserObject(user,role);  
                let jwtToken  = await createJWT(jwtObject);
                    authKey.userKeyEntity=user.dataValues.Id;
                    authKey.token=jwtToken;
            let redisDb   = await AddJWTToRedis(user.dataValues.Id, jwtToken);
                context.body = {
                    success:true,
                    token:jwtToken
                };   
        }
    }      
    }catch(ex){
            response.value={error: `${ex}`}
        context.body=response;
    }
}
 /**
  * Генерация объекта для создания JWT токена
  * Generate object for JWT token creation
  * @param {*} object 
  * @param {*} role 
  * @returns 
  */
export async function fillJWTUserObject(object,role)
{
  let userObject={};
    if(object)
    {
        const { RoleId, Password, ...restDataValues } = object.dataValues;
        userObject = {
           ...restDataValues,
        Role: role.dataValues.Name,
        };
    }
    return userObject;
}

export async function getRedisValue(authKey)
{
    if(authKey.token)
    {
       let compareToken = redis.RedisGetValue(authKey.token);
        return compareToken === 1;
    }
}   
/**
 * Добавление JWT токена в Redis 
 * Set up JWT token to the redis 
 * @param {*} user 
 * @param {*} token 
 */
export async function AddJWTToRedis(user, token) {
    try {
        if (user || token) {
            redis.RedisSetValue("authUser_"+v4() + user, JSON.stringify(authKey),7892928);
        }
    } catch (ex) {
       console.error(ex);
    }
}
