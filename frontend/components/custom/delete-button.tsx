import { toast } from "@/hooks/use-toast";
import { Button } from "../ui/button";

const DeleteButton = () =>
{
    return (
        <Button variant="destructive" onClick={() => {
            toast({
                title: "Deleting your domain record",
                description: "I will reload the page once it is done"
            })
        }}
        >
            Delete</Button>
    )
};

export default DeleteButton;