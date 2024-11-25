import { TestBed, ComponentFixture } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { RouterTestingModule } from '@angular/router/testing';  // Import RouterTestingModule to test routing
import { HttpClientModule } from '@angular/common/http';         // Import HttpClientModule for HTTP requests (if needed)

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(async () => {
    // Configure the testing module
    await TestBed.configureTestingModule({
      declarations: [AppComponent],           // Declare the AppComponent
      imports: [RouterTestingModule, HttpClientModule]  // Import necessary modules
    })
      .compileComponents();  // Compile components before running tests

    // Create a fixture for the AppComponent
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;  // Get the component instance
    fixture.detectChanges();  // Trigger change detection to update the view
  });

  it('should create the app', () => {
    // Check if the AppComponent is created successfully
    expect(component).toBeTruthy();
  });

  it('should have a title "Angular Application"', () => {
    // Test if the component's title is set correctly
    expect(component.title).toBe('Angular Application');
  });

  it('should render the title in the template', () => {
    // Test if the title is rendered in the template
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Product List');
  });

  // Optionally test routing or any other functionality
  it('should navigate to products page when products link is clicked', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    const productsLink = compiled.querySelector('a[routerLink="/products"]')!;
    expect(productsLink).toBeTruthy();  // Check if the link exists
  });
});
