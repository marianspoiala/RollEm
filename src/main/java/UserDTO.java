import org.json.simple.JSONObject;

/**
 * Created by Marian on 10/24/2015.
 */
public class UserDTO {
    private String name;
    private Long score;

    public Long getScore() {
        return score;
    }

    public void setScore(Long score) {
        this.score = score;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    @Override
    public String toString() {
        return this.getName() + " " + this.getScore();
    }
}
