namespace Models;
public class Todo{
    public int id{get;set;}
    public string description{get;set;}
    public bool is_complete{get;set;}
    public Todo(string description){
            this.description=description;
        }
    public Todo(int id, string description, bool is_complete){
        this.id=id;
        this.description=description;
        this.is_complete=is_complete;
    }
    
    public override string ToString(){
        return $"id: {this.id}, description: {this.description}, is_complete: {this.is_complete}";
    }

}
