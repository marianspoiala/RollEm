/**
 * Created by Marian on 10/24/2015.
 */

import org.json.simple.JSONObject;

public class UserSerializer {

    public static JSONObject toJSON(UserDTO userDTO) {
        JSONObject jsonObject = new JSONObject();
        jsonObject.put("name", userDTO.getName());
        jsonObject.put("score", userDTO.getScore());
        return jsonObject;
    }

    public static UserDTO toDTO(JSONObject jsonObject) {
        UserDTO userDTO = new UserDTO();
        userDTO.setName(jsonObject.get("name").toString());
        userDTO.setScore((Long)jsonObject.get("score"));
        return userDTO;
    }
}
