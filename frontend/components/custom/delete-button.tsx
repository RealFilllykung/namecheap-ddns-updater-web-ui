import { toast } from "@/hooks/use-toast";
import { Button } from "../ui/button";

export default function DeleteButton()
{
    return (
        <Button variant="destructive" onClick={() => {
            toast({
                title: "Deleting your domain record",
                description: "I will let you know when the deletion is done"
            })
        }}
        >
            Delete</Button>
    )
}