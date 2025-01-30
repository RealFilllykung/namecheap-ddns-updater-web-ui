import { toast } from "@/hooks/use-toast";
import { Button } from "../ui/button";

export default function SaveButton()
{
    return (
        <Button onClick={() => {
            toast({
                title: "Saving your new domain record",
                description: "Please wait for a moment... the server is trying its best!"
            })
        }}
        >
            Save</Button>
    )
}